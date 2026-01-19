import React from 'react'
import { Helmet } from 'react-helmet'

import Navbar from '../components/Navbar'
import Footer from '../components/Footer'

import navbar from '../data/navbar.json'

import './Layout.scss'

const Layout = ({children, title, page, hideFooter = false, hideNavbar = false}) => {

  return (
    <div className={`Application ${hideFooter ? 'no-footer' : ''}`}>
      <Helmet>
        <meta charSet="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>{title}</title>
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link rel="preconnect" href="https://fonts.gstatic.com" crossOrigin="anonymous" />
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800&family=JetBrains+Mono:wght@400;500&display=swap" rel="stylesheet" />
      </Helmet>
      {!hideNavbar && <Navbar option={page} items={navbar}/>}
      <main className="MainContent">
        {children}
      </main>
      {!hideFooter && <Footer />}
    </div>
  )
}

export default Layout