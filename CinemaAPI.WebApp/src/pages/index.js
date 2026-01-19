import React from "react"
import Layout from "../components/Layout"
import { Link } from "gatsby"
import "./index.scss"

import { Button } from '@mui/material'
import { FaFilm, FaUsers, FaCode, FaRocket, FaLock, FaChartLine } from 'react-icons/fa'

import LandingPicture from '../images/index/landing.svg'

const FeatureCard = ({ icon, title, description }) => (
  <div className="FeatureCard">
    <div className="FeatureIcon">{icon}</div>
    <h3>{title}</h3>
    <p>{description}</p>
  </div>
)

const StatItem = ({ value, label }) => (
  <div className="StatItem">
    <h3>{value}</h3>
    <p>{label}</p>
  </div>
)

export default function Home() {
  return (
    <Layout title="Cinema API | Home" page={1}>
      <div className="Home">
        
        <div className="HomeTop">
          <img src={LandingPicture} alt="Cinema API illustration" />
          <div className="HomeTopRight">
            <h1>The <span>Cinema API</span> for Modern Apps</h1>
            <p>Build powerful entertainment applications with our comprehensive movie database API. 
              Access films, actors, ratings, and more with lightning-fast response times.</p>
            <div className="HomeButtons">
              <Link to="/signup">
                <Button variant="contained" color="primary">Get Started Free</Button>
              </Link>
              <Link to="/docs">
                <Button variant="outlined">View Documentation</Button>
              </Link>
            </div>
          </div>
        </div>

        <div className="HomeStats">
          <StatItem value="10,000+" label="Movies" />
          <StatItem value="50,000+" label="Actors" />
          <StatItem value="99.9%" label="Uptime" />
          <StatItem value="<100ms" label="Response" />
        </div>

        <div className="HomeFeatures">
          <h2>Built for Developers</h2>
          <p>Everything you need to integrate movie data into your applications</p>
          <div className="FeaturesGrid">
            <FeatureCard 
              icon={<FaFilm />}
              title="Rich Movie Data"
              description="Comprehensive movie information including synopsis, cast, crew, ratings, and box office data."
            />
            <FeatureCard 
              icon={<FaUsers />}
              title="Person Database"
              description="Detailed profiles of actors, directors, and crew with full filmography data."
            />
            <FeatureCard 
              icon={<FaCode />}
              title="RESTful Design"
              description="Clean, intuitive endpoints following REST best practices. Easy integration."
            />
            <FeatureCard 
              icon={<FaRocket />}
              title="High Performance"
              description="Sub-100ms response times. Handle thousands of requests per second."
            />
            <FeatureCard 
              icon={<FaLock />}
              title="Secure Access"
              description="JWT authentication with role-based access control for your data."
            />
            <FeatureCard 
              icon={<FaChartLine />}
              title="Analytics Dashboard"
              description="Monitor API usage with detailed analytics and performance metrics."
            />
          </div>
        </div>

        <div className="HomeCTA">
          <h2>Ready to Start Building?</h2>
          <p>Join developers using Cinema API to power their entertainment applications.</p>
          <Link to="/signup">
            <Button variant="contained" size="large">Create Free Account</Button>
          </Link>
        </div>

      </div>
    </Layout>
  )
}
