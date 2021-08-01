import React from 'react'
import {FaHeart} from 'react-icons/fa'
import Tippy from '@tippyjs/react'

import './Footer.scss'

const Footer = () => {
    return(
        <footer>
            <p>®Frank Orozco 2021 - All rights reserved</p>
            <div className="Row">
                <p>Built with</p>
                <FaHeart />
                <p>using</p>
                <div className="Technologies">
                    <a href='https://es.reactjs.org/' target="_blank"><i className="devicon-react-original colored"></i></a>
                    <p>and</p>
                    <a href='https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1' target="_blank"><i className="devicon-dotnetcore-plain colored"></i></a>
                </div>
            </div>
        </footer>
    )
}

export default Footer