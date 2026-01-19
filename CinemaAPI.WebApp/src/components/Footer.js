import React from 'react'
import { Link } from 'gatsby'
import { FaGithub, FaTwitter, FaLinkedin, FaHeart } from 'react-icons/fa'
import { MdEmail } from 'react-icons/md'

import './Footer.scss'

const Footer = () => {
    const currentYear = new Date().getFullYear()
    
    return (
        <footer className="Footer">
            <div className="FooterContent">
                <div className="FooterMain">
                    <div className="FooterBrand">
                        <h3>Cinema API</h3>
                        <p>The premier REST API for movie and entertainment data. Build powerful applications with comprehensive film information.</p>
                        <div className="FooterSocial">
                            <a href="https://github.com" target="_blank" rel="noreferrer" aria-label="GitHub">
                                <FaGithub />
                            </a>
                            <a href="https://twitter.com" target="_blank" rel="noreferrer" aria-label="Twitter">
                                <FaTwitter />
                            </a>
                            <a href="https://linkedin.com" target="_blank" rel="noreferrer" aria-label="LinkedIn">
                                <FaLinkedin />
                            </a>
                            <a href="mailto:contact@cinemaapi.com" aria-label="Email">
                                <MdEmail />
                            </a>
                        </div>
                    </div>
                    
                    <div className="FooterLinks">
                        <div className="FooterLinkGroup">
                            <h4>Product</h4>
                            <Link to="/docs">Documentation</Link>
                            <Link to="/status">System Status</Link>
                            <Link to="/dashboard">Dashboard</Link>
                        </div>
                        
                        <div className="FooterLinkGroup">
                            <h4>Company</h4>
                            <Link to="/">About Us</Link>
                            <Link to="/">Blog</Link>
                            <Link to="/">Careers</Link>
                        </div>
                        
                        <div className="FooterLinkGroup">
                            <h4>Legal</h4>
                            <Link to="/">Privacy Policy</Link>
                            <Link to="/">Terms of Service</Link>
                            <Link to="/">Cookie Policy</Link>
                        </div>
                    </div>
                </div>
                
                <div className="FooterBottom">
                    <p>© {currentYear} Cinema API. All rights reserved.</p>
                    <p className="FooterCredit">
                        Built with <FaHeart className="HeartIcon" /> using React & .NET Core
                    </p>
                </div>
            </div>
        </footer>
    )
}

export default Footer