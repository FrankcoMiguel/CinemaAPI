import React, { useState, useEffect } from "react"
import { navigate } from "gatsby"
import Layout from "../components/Layout"
import { useAuth } from "../context/AuthContext"
import "./dashboard.scss"

import { Button, TextField, Dialog, DialogTitle, DialogContent, DialogActions } from '@mui/material'
import { FaFilm, FaUsers, FaChartLine, FaPlus, FaEdit, FaTrash, FaSignOutAlt } from 'react-icons/fa'
import { MdMovie, MdPerson, MdStar } from 'react-icons/md'

import api from '../data/api'

// Protected Route wrapper
const ProtectedContent = ({ children }) => {
    const { isAuthenticated, loading } = useAuth()

    useEffect(() => {
        if (!loading && !isAuthenticated()) {
            navigate('/signin')
        }
    }, [loading, isAuthenticated])

    if (loading) {
        return (
            <div className="DashboardLoading">
                <div className="Spinner"></div>
                <p>Loading...</p>
            </div>
        )
    }

    if (!isAuthenticated()) {
        return null
    }

    return children
}

const StatCard = ({ icon, title, value, color }) => (
    <div className="StatCard" style={{ borderLeftColor: color }}>
        <div className="StatIcon" style={{ color }}>{icon}</div>
        <div className="StatInfo">
            <h3>{value}</h3>
            <p>{title}</p>
        </div>
    </div>
)

const FilmTable = ({ films, onEdit, onDelete }) => (
    <div className="DataTable">
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Title</th>
                    <th>Year</th>
                    <th>Language</th>
                    <th>Score</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {films.map(film => (
                    <tr key={film.id}>
                        <td>{film.id}</td>
                        <td>{film.title}</td>
                        <td>{film.releaseYear}</td>
                        <td>{film.language}</td>
                        <td>{film.score || 'N/A'}</td>
                        <td className="Actions">
                            <button onClick={() => onEdit(film)} title="Edit">
                                <FaEdit />
                            </button>
                            <button onClick={() => onDelete(film.id)} title="Delete" className="delete">
                                <FaTrash />
                            </button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
)

export default function Dashboard() {
    const { user, token, logout } = useAuth()
    const [films, setFilms] = useState([])
    const [stats, setStats] = useState({ films: 0, genres: 0, persons: 0 })
    const [loading, setLoading] = useState(true)
    const [dialogOpen, setDialogOpen] = useState(false)
    const [currentFilm, setCurrentFilm] = useState(null)
    const [formData, setFormData] = useState({
        title: '', synopsis: '', releaseYear: '', runningTime: '', language: ''
    })

    const fetchData = async () => {
        if (!token) return
        
        try {
            const [filmsRes] = await Promise.all([
                api.get('/film', { headers: { Authorization: `Bearer ${token}` } })
            ])
            
            setFilms(filmsRes.data.data || [])
            setStats({
                films: filmsRes.data.meta?.totalCount || filmsRes.data.data?.length || 0,
                genres: 12,
                persons: 156
            })
        } catch (error) {
            console.error('Error fetching data:', error)
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        fetchData()
    }, [token])

    const handleLogout = () => {
        logout()
        navigate('/')
    }

    const handleEdit = (film) => {
        setCurrentFilm(film)
        setFormData({
            title: film.title,
            synopsis: film.synopsis || '',
            releaseYear: film.releaseYear,
            runningTime: film.runningTime || '',
            language: film.language || ''
        })
        setDialogOpen(true)
    }

    const handleAdd = () => {
        setCurrentFilm(null)
        setFormData({ title: '', synopsis: '', releaseYear: '', runningTime: '', language: '' })
        setDialogOpen(true)
    }

    const handleSave = async () => {
        try {
            if (currentFilm) {
                await api.put(`/film/${currentFilm.id}`, formData, {
                    headers: { Authorization: `Bearer ${token}` }
                })
            } else {
                await api.post('/film', formData, {
                    headers: { Authorization: `Bearer ${token}` }
                })
            }
            setDialogOpen(false)
            fetchData()
        } catch (error) {
            console.error('Error saving film:', error)
            alert('Error saving film')
        }
    }

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this film?')) {
            try {
                await api.delete(`/film/${id}`, {
                    headers: { Authorization: `Bearer ${token}` }
                })
                fetchData()
            } catch (error) {
                console.error('Error deleting film:', error)
                alert('Error deleting film')
            }
        }
    }

    return (
        <Layout title="Cinema API | Dashboard" page={5}>
            <ProtectedContent>
                <div className="Dashboard">
                    <div className="DashboardHeader">
                        <div className="HeaderLeft">
                            <h1>Dashboard</h1>
                            <p>Welcome back, {user?.username || 'User'}!</p>
                        </div>
                        <Button 
                            variant="outlined" 
                            color="error" 
                            startIcon={<FaSignOutAlt />}
                            onClick={handleLogout}
                        >
                            Sign Out
                        </Button>
                    </div>

                    <div className="StatsGrid">
                        <StatCard 
                            icon={<MdMovie />} 
                            title="Total Films" 
                            value={stats.films} 
                            color="#3498db" 
                        />
                        <StatCard 
                            icon={<MdStar />} 
                            title="Genres" 
                            value={stats.genres} 
                            color="#e74c3c" 
                        />
                        <StatCard 
                            icon={<MdPerson />} 
                            title="Persons" 
                            value={stats.persons} 
                            color="#2ecc71" 
                        />
                        <StatCard 
                            icon={<FaChartLine />} 
                            title="API Calls Today" 
                            value="1,234" 
                            color="#9b59b6" 
                        />
                    </div>

                    <div className="DataSection">
                        <div className="SectionHeader">
                            <h2><FaFilm /> Films Management</h2>
                            <Button 
                                variant="contained" 
                                startIcon={<FaPlus />}
                                onClick={handleAdd}
                                sx={{ backgroundColor: '#fcaf3b', '&:hover': { backgroundColor: '#e09a2f' } }}
                            >
                                Add Film
                            </Button>
                        </div>

                        {loading ? (
                            <div className="TableLoading">Loading films...</div>
                        ) : films.length > 0 ? (
                            <FilmTable films={films} onEdit={handleEdit} onDelete={handleDelete} />
                        ) : (
                            <div className="NoData">
                                <FaFilm />
                                <p>No films found. Add your first film!</p>
                            </div>
                        )}
                    </div>

                    <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} maxWidth="sm" fullWidth>
                        <DialogTitle>{currentFilm ? 'Edit Film' : 'Add New Film'}</DialogTitle>
                        <DialogContent>
                            <div className="DialogForm">
                                <TextField
                                    label="Title"
                                    value={formData.title}
                                    onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                                    fullWidth
                                    margin="normal"
                                    required
                                />
                                <TextField
                                    label="Synopsis"
                                    value={formData.synopsis}
                                    onChange={(e) => setFormData({ ...formData, synopsis: e.target.value })}
                                    fullWidth
                                    margin="normal"
                                    multiline
                                    rows={3}
                                />
                                <TextField
                                    label="Release Year"
                                    value={formData.releaseYear}
                                    onChange={(e) => setFormData({ ...formData, releaseYear: e.target.value })}
                                    fullWidth
                                    margin="normal"
                                    type="number"
                                />
                                <TextField
                                    label="Running Time (minutes)"
                                    value={formData.runningTime}
                                    onChange={(e) => setFormData({ ...formData, runningTime: e.target.value })}
                                    fullWidth
                                    margin="normal"
                                    type="number"
                                />
                                <TextField
                                    label="Language"
                                    value={formData.language}
                                    onChange={(e) => setFormData({ ...formData, language: e.target.value })}
                                    fullWidth
                                    margin="normal"
                                />
                            </div>
                        </DialogContent>
                        <DialogActions>
                            <Button onClick={() => setDialogOpen(false)}>Cancel</Button>
                            <Button onClick={handleSave} variant="contained" sx={{ backgroundColor: '#fcaf3b' }}>
                                Save
                            </Button>
                        </DialogActions>
                    </Dialog>
                </div>
            </ProtectedContent>
        </Layout>
    )
}
