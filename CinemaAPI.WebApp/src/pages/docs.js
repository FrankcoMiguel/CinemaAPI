import React, { useState } from "react"
import Layout from "../components/Layout"
import "./docs.scss"

import { FaFilm, FaUser, FaStar, FaTheaterMasks, FaLock, FaCode } from 'react-icons/fa'
import { MdExpandMore, MdExpandLess } from 'react-icons/md'

const endpoints = [
    {
        category: "Films",
        icon: <FaFilm />,
        description: "Manage movie information",
        endpoints: [
            { method: "GET", path: "/api/film", description: "Get all films with pagination", auth: true },
            { method: "GET", path: "/api/film/{id}", description: "Get a specific film by ID", auth: true },
            { method: "POST", path: "/api/film", description: "Create a new film", auth: true },
            { method: "PUT", path: "/api/film/{id}", description: "Update an existing film", auth: true },
            { method: "DELETE", path: "/api/film/{id}", description: "Delete a film", auth: true },
        ]
    },
    {
        category: "Genres",
        icon: <FaTheaterMasks />,
        description: "Manage film genres",
        endpoints: [
            { method: "GET", path: "/api/genre", description: "Get all genres", auth: true },
            { method: "GET", path: "/api/genre/{id}", description: "Get a specific genre by ID", auth: true },
            { method: "POST", path: "/api/genre", description: "Create a new genre", auth: true },
            { method: "PUT", path: "/api/genre/{id}", description: "Update an existing genre", auth: true },
            { method: "DELETE", path: "/api/genre/{id}", description: "Delete a genre", auth: true },
        ]
    },
    {
        category: "Cast & Crew",
        icon: <FaUser />,
        description: "Manage actors, directors, and crew",
        endpoints: [
            { method: "GET", path: "/api/person", description: "Get all persons", auth: true },
            { method: "GET", path: "/api/person/{id}", description: "Get a specific person by ID", auth: true },
            { method: "POST", path: "/api/person", description: "Create a new person", auth: true },
            { method: "PUT", path: "/api/person/{id}", description: "Update an existing person", auth: true },
            { method: "DELETE", path: "/api/person/{id}", description: "Delete a person", auth: true },
        ]
    },
    {
        category: "Ratings",
        icon: <FaStar />,
        description: "Manage film ratings (G, PG, PG-13, R, etc.)",
        endpoints: [
            { method: "GET", path: "/api/rating", description: "Get all ratings", auth: true },
            { method: "GET", path: "/api/rating/{id}", description: "Get a specific rating by ID", auth: true },
            { method: "POST", path: "/api/rating", description: "Create a new rating", auth: true },
        ]
    },
    {
        category: "Authentication",
        icon: <FaLock />,
        description: "User authentication and tokens",
        endpoints: [
            { method: "POST", path: "/api/token", description: "Generate JWT token", auth: false },
        ]
    }
]

const MethodBadge = ({ method }) => {
    const colors = {
        GET: '#61affe',
        POST: '#49cc90',
        PUT: '#fca130',
        DELETE: '#f93e3e',
        PATCH: '#50e3c2'
    }
    
    return (
        <span className="MethodBadge" style={{ backgroundColor: colors[method] }}>
            {method}
        </span>
    )
}

const EndpointCategory = ({ category, icon, description, endpoints }) => {
    const [expanded, setExpanded] = useState(false)

    return (
        <div className="EndpointCategory">
            <div className="CategoryHeader" onClick={() => setExpanded(!expanded)}>
                <div className="CategoryLeft">
                    <span className="CategoryIcon">{icon}</span>
                    <div className="CategoryInfo">
                        <h3>{category}</h3>
                        <p>{description}</p>
                    </div>
                </div>
                <span className="ExpandIcon">
                    {expanded ? <MdExpandLess /> : <MdExpandMore />}
                </span>
            </div>
            {expanded && (
                <div className="EndpointsList">
                    {endpoints.map((endpoint, idx) => (
                        <div key={idx} className="EndpointItem">
                            <MethodBadge method={endpoint.method} />
                            <code className="EndpointPath">{endpoint.path}</code>
                            <span className="EndpointDescription">{endpoint.description}</span>
                            {endpoint.auth && <FaLock className="AuthIcon" title="Requires authentication" />}
                        </div>
                    ))}
                </div>
            )}
        </div>
    )
}

export default function Docs() {
    return (
        <Layout title="Cinema API | Documentation" page={2}>
            <div className="Docs">
                <div className="DocsHeader">
                    <FaCode className="DocsIcon" />
                    <h1>API Documentation</h1>
                    <p>Complete reference for the Cinema API endpoints</p>
                </div>

                <div className="DocsContent">
                    <div className="DocsIntro">
                        <h2>Getting Started</h2>
                        <p>
                            The Cinema API provides a RESTful interface for accessing movie, actor, 
                            and rating information. All endpoints return JSON responses.
                        </p>
                        
                        <div className="BaseUrl">
                            <strong>Base URL:</strong>
                            <code>https://localhost:44389/api</code>
                        </div>

                        <div className="AuthInfo">
                            <h3><FaLock /> Authentication</h3>
                            <p>
                                Most endpoints require JWT authentication. Obtain a token by sending 
                                your credentials to <code>/api/token</code>, then include it in the 
                                Authorization header:
                            </p>
                            <pre>
{`Authorization: Bearer <your_token>`}
                            </pre>
                        </div>
                    </div>

                    <div className="EndpointsSection">
                        <h2>Endpoints</h2>
                        {endpoints.map((cat, idx) => (
                            <EndpointCategory key={idx} {...cat} />
                        ))}
                    </div>

                    <div className="ResponseSection">
                        <h2>Response Format</h2>
                        <p>All API responses follow this structure:</p>
                        <pre>
{`{
  "data": { ... },
  "meta": {
    "currentPage": 1,
    "pageSize": 10,
    "totalCount": 100,
    "totalPages": 10,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}`}
                        </pre>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
