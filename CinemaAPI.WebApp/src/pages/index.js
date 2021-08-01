import React from "react"
import Layout from "../components/Layout"
import { Link } from "gatsby"
import "./index.scss"

import { Button } from '@material-ui/core'

import LandingPicture from '../images/index/landing.svg'


export default function Home() {
  return (
    <Layout title="Cinema API | Home" page={1}>
      <div className="Home">
        
        <div className="HomeTop">
          <img src={LandingPicture} alt="Landing Picture" />
          <div className="HomeTopRight">
            <h1>Cinema Premium API</h1>
            <p>A REST API that collect a wide 
              variety of Cinema information 
              from movies to actors A REST API that collect a wide 
              variety of Cinema information 
              from movies to actors A REST API that collect a wide 
              variety of Cinema information 
              from movies to actors.</p>
            <Link to="/signup">
              <Button variant="contained" color="primary">Join us</Button>
            </Link>
          </div>
        </div>
        

      </div>
    </Layout>
  )
}
