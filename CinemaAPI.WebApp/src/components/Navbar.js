import React, { useState, useEffect } from 'react'
import { Link } from 'gatsby'

import './Navbar.scss'
import Logotype from '../images/logotype.svg'

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faBars, faTimes } from '@fortawesome/free-solid-svg-icons'
import { useAuth } from '../context/AuthContext'

function NavigationBar({option, items = []}) {

  const { isAuthenticated } = useAuth()
  const [menu, showMenu] = useState(false)

  const toggle = () => showMenu(!menu)

  useEffect(() => {
    document.body.style.overflow = menu ? 'hidden' : 'scroll'
    return () => {
      document.body.style.overflow = 'unset'
    }
  }, [menu])


  return (
    <nav>
      <div className={`NavContainer ${menu ? 'open' : ''}`}>
        <div className="NavTop">
          <img className="NavLogo" src={Logotype} alt="Cinema API Logo" />
          <FontAwesomeIcon className="NavHamburger" icon={menu ? faTimes : faBars} onClick={toggle} />
        </div>
        <ul className={`NavContent ${menu ? 'shown' : 'hidden'}`}>
          {items.map(o => (
            <li key={o.id} className={o.id === option ? 'NavItem active' : 'NavItem'}>
              {o.name !== 'Home' ?
              <Link to={o.url}>{o.name}</Link>
              :
              <a href={o.url}>{o.name}</a>}
            </li>
          ))}
            {isAuthenticated() ? (
              <li className={5 === option ? 'NavButton active': 'NavButton'}>
                <Link to="/dashboard"><button>Dashboard</button></Link>
              </li>
            ) : (
              <li className={3 === option ? 'NavButton active': 'NavButton'}>
                <Link to="/signin"><button>Sign in</button></Link>
              </li>
            )}
        </ul>
      </div>
    </nav>
  )
}

export default NavigationBar