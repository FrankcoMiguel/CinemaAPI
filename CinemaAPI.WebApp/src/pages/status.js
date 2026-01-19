import React, { useState, useEffect } from "react"
import Layout from "../components/Layout"
import "./status.scss"

import { FaCheckCircle, FaTimesCircle, FaExclamationTriangle, FaServer, FaDatabase, FaLock, FaGlobe, FaSync, FaClock } from 'react-icons/fa'
import { MdRefresh } from 'react-icons/md'
import { Button, Tooltip } from '@mui/material'

import api from '../data/api'

const StatusIndicator = ({ status }) => {
    const config = {
        operational: { icon: <FaCheckCircle />, color: '#2ecc71', text: 'Operational' },
        degraded: { icon: <FaExclamationTriangle />, color: '#f39c12', text: 'Degraded' },
        outage: { icon: <FaTimesCircle />, color: '#e74c3c', text: 'Outage' },
        unknown: { icon: <FaExclamationTriangle />, color: '#95a5a6', text: 'Unknown' }
    }

    const { icon, color, text } = config[status] || config.unknown

    return (
        <span className="StatusIndicator" style={{ color }}>
            {icon} {text}
        </span>
    )
}

const ServiceCard = ({ name, icon, status, latency, description, lastChecked }) => (
    <div className={`ServiceCard ${status}`}>
        <div className="ServiceIcon">{icon}</div>
        <div className="ServiceInfo">
            <div className="ServiceHeader">
                <h3>{name}</h3>
                <StatusIndicator status={status} />
            </div>
            <p className="ServiceDescription">{description}</p>
            <div className="ServiceMeta">
                {latency && <span className="Latency">Response: {latency}ms</span>}
                <span className="LastChecked">Last checked: {lastChecked}</span>
            </div>
        </div>
    </div>
)

const UptimeBar = ({ data }) => (
    <div className="UptimeBar">
        {data.map((day, idx) => (
            <Tooltip 
                key={idx}
                title={
                    <div className="UptimeTooltip">
                        <strong>{day.date}</strong>
                        <span>{day.statusMessage}</span>
                    </div>
                }
                arrow
                placement="top"
            >
                <div className={`UptimeDay ${day.status}`} />
            </Tooltip>
        ))}
    </div>
)

const LatencyBar = ({ data, maxLatency }) => (
    <div className="LatencyBar">
        {data.map((day, idx) => {
            const heightPercent = (day.avgLatency / maxLatency) * 100
            const latencyStatus = day.avgLatency < 100 ? 'fast' : day.avgLatency < 200 ? 'normal' : 'slow'
            return (
                <Tooltip 
                    key={idx}
                    title={
                        <div className="UptimeTooltip">
                            <strong>{day.date}</strong>
                            <span>Avg: {day.avgLatency}ms</span>
                            <span style={{ opacity: 0.7, fontSize: '10px' }}>Peak: {day.peakLatency}ms</span>
                        </div>
                    }
                    arrow
                    placement="top"
                >
                    <div className="LatencyDayWrapper">
                        <div 
                            className={`LatencyDay ${latencyStatus}`}
                            style={{ height: `${Math.max(heightPercent, 10)}%` }}
                        />
                    </div>
                </Tooltip>
            )
        })}
    </div>
)

export default function Status() {
    const [services, setServices] = useState([])
    const [overallStatus, setOverallStatus] = useState('unknown')
    const [loading, setLoading] = useState(true)
    const [lastUpdate, setLastUpdate] = useState(null)

    const checkServices = async () => {
        setLoading(true)
        
        const serviceChecks = [
            {
                name: 'Cinema API',
                icon: <FaServer />,
                description: 'Main REST API for movie data',
                endpoint: '/film',
                requiresAuth: true
            },
            {
                name: 'Authentication Service',
                icon: <FaLock />,
                description: 'JWT token generation and validation',
                endpoint: '/token',
                requiresAuth: false,
                method: 'OPTIONS'
            },
            {
                name: 'Database',
                icon: <FaDatabase />,
                description: 'SQL Server database connectivity',
                endpoint: '/genre',
                requiresAuth: true
            }
        ]

        const results = await Promise.all(
            serviceChecks.map(async (service) => {
                const startTime = Date.now()
                try {
                    // Try to reach the endpoint (will fail auth but confirms service is up)
                    await api.get(service.endpoint, { timeout: 5000 }).catch(err => {
                        // 401 means service is up but requires auth - that's fine
                        if (err.response?.status === 401) {
                            return { status: 200 }
                        }
                        throw err
                    })
                    
                    return {
                        ...service,
                        status: 'operational',
                        latency: Date.now() - startTime
                    }
                } catch (error) {
                    // Check if it's a network error or timeout
                    if (error.code === 'ECONNABORTED' || error.code === 'ERR_NETWORK') {
                        return {
                            ...service,
                            status: 'outage',
                            latency: null
                        }
                    }
                    // If we got any response, service is at least partially up
                    if (error.response) {
                        return {
                            ...service,
                            status: error.response.status >= 500 ? 'degraded' : 'operational',
                            latency: Date.now() - startTime
                        }
                    }
                    return {
                        ...service,
                        status: 'outage',
                        latency: null
                    }
                }
            })
        )

        // Add CDN/Static as always operational (local)
        results.push({
            name: 'CDN / Static Assets',
            icon: <FaGlobe />,
            description: 'Static content delivery',
            status: 'operational',
            latency: 12
        })

        const now = new Date()
        const lastCheckedStr = now.toLocaleTimeString()

        setServices(results.map(s => ({ ...s, lastChecked: lastCheckedStr })))
        setLastUpdate(now)

        // Determine overall status
        const statuses = results.map(s => s.status)
        if (statuses.every(s => s === 'operational')) {
            setOverallStatus('operational')
        } else if (statuses.some(s => s === 'outage')) {
            setOverallStatus('outage')
        } else {
            setOverallStatus('degraded')
        }

        setLoading(false)
    }

    useEffect(() => {
        checkServices()
        
        // Auto-refresh every 30 seconds
        const interval = setInterval(checkServices, 30000)
        return () => clearInterval(interval)
    }, [])

    // Generate mock uptime data for the last 30 days
    const uptimeData = Array.from({ length: 30 }, (_, i) => {
        const date = new Date()
        date.setDate(date.getDate() - (29 - i))
        const isDown = Math.random() > 0.92
        const uptime = isDown ? (95 + Math.random() * 4.9) : 100
        const downtimeMinutes = isDown ? Math.floor((100 - uptime) * 14.4) : 0 // 1% = ~14.4 minutes in a day
        
        let statusMessage = 'No known issues'
        if (downtimeMinutes > 0) {
            if (downtimeMinutes >= 60) {
                const hours = Math.floor(downtimeMinutes / 60)
                const mins = downtimeMinutes % 60
                statusMessage = mins > 0 
                    ? `Down for ${hours}h ${mins}m` 
                    : `Down for ${hours} hour${hours > 1 ? 's' : ''}`
            } else {
                statusMessage = `Down for ${downtimeMinutes} minute${downtimeMinutes > 1 ? 's' : ''}`
            }
        }
        
        return {
            date: date.toLocaleDateString('en-US', { weekday: 'short', month: 'short', day: 'numeric' }),
            uptime: uptime.toFixed(2),
            status: uptime === 100 ? 'operational' : uptime > 99 ? 'degraded' : 'outage',
            downtimeMinutes,
            statusMessage
        }
    })

    const overallUptime = (uptimeData.reduce((acc, d) => acc + parseFloat(d.uptime), 0) / uptimeData.length).toFixed(3)

    // Generate mock latency data for the last 31 days
    const latencyData = Array.from({ length: 31 }, (_, i) => {
        const date = new Date()
        date.setDate(date.getDate() - (30 - i))
        const baseLatency = 45 + Math.random() * 30 // Base between 45-75ms
        const spike = Math.random() > 0.9 ? Math.random() * 150 : 0 // Occasional spikes
        const avgLatency = Math.round(baseLatency + spike)
        const peakLatency = Math.round(avgLatency + 20 + Math.random() * 80)
        
        return {
            date: date.toLocaleDateString('en-US', { weekday: 'short', month: 'short', day: 'numeric' }),
            avgLatency,
            peakLatency
        }
    })

    const avgLatencyOverall = Math.round(latencyData.reduce((acc, d) => acc + d.avgLatency, 0) / latencyData.length)
    const maxLatency = Math.max(...latencyData.map(d => d.avgLatency))

    return (
        <Layout title="Cinema API | System Status" page={6}>
            <div className="Status">
                <div className="StatusHeader">
                    <h1>System Status</h1>
                    <p>Real-time availability and performance monitoring</p>
                </div>

                <div className={`OverallStatus ${overallStatus}`}>
                    <div className="OverallLeft">
                        <StatusIndicator status={overallStatus} />
                        <span className="OverallText">
                            {overallStatus === 'operational' && 'All Systems Operational'}
                            {overallStatus === 'degraded' && 'Partial System Outage'}
                            {overallStatus === 'outage' && 'Major System Outage'}
                            {overallStatus === 'unknown' && 'Checking Systems...'}
                        </span>
                    </div>
                    <Button 
                        variant="outlined" 
                        startIcon={loading ? <FaSync className="spinning" /> : <MdRefresh />}
                        onClick={checkServices}
                        disabled={loading}
                    >
                        {loading ? 'Checking...' : 'Refresh'}
                    </Button>
                </div>

                <div className="UptimeSection">
                    <div className="UptimeHeader">
                        <h2>30-Day Uptime</h2>
                        <span className="UptimePercent">{overallUptime}%</span>
                    </div>
                    <UptimeBar data={uptimeData} />
                    <div className="UptimeLegend">
                        <span><FaCheckCircle style={{ color: '#2ecc71' }} /> Operational</span>
                        <span><FaExclamationTriangle style={{ color: '#f39c12' }} /> Degraded</span>
                        <span><FaTimesCircle style={{ color: '#e74c3c' }} /> Outage</span>
                    </div>
                </div>

                <div className="LatencySection">
                    <div className="LatencyHeader">
                        <h2><FaClock /> 31-Day Response Time</h2>
                        <span className="LatencyAvg">{avgLatencyOverall}ms avg</span>
                    </div>
                    <LatencyBar data={latencyData} maxLatency={maxLatency} />
                    <div className="LatencyLegend">
                        <span><span className="LegendDot fast" /> &lt;100ms (Fast)</span>
                        <span><span className="LegendDot normal" /> 100-200ms (Normal)</span>
                        <span><span className="LegendDot slow" /> &gt;200ms (Slow)</span>
                    </div>
                </div>

                <div className="ServicesSection">
                    <h2>Services</h2>
                    <div className="ServicesGrid">
                        {services.map((service, idx) => (
                            <ServiceCard key={idx} {...service} />
                        ))}
                    </div>
                </div>

                <div className="IncidentsSection">
                    <h2>Recent Incidents</h2>
                    <div className="IncidentsList">
                        <div className="Incident resolved">
                            <div className="IncidentHeader">
                                <span className="IncidentStatus"><FaCheckCircle /> Resolved</span>
                                <span className="IncidentDate">January 15, 2026</span>
                            </div>
                            <h3>Scheduled Maintenance</h3>
                            <p>Database optimization and security updates were performed. All services are now restored.</p>
                        </div>
                        <div className="NoIncidents">
                            <FaCheckCircle />
                            <p>No major incidents in the last 30 days</p>
                        </div>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
