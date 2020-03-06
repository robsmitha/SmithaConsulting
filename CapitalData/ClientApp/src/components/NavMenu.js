import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { Authentication } from '../services/authentication'

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            authenticated: Authentication.getUserId() > 0
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }


    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" dark color="dark">
                    <Container>
                        <NavbarBrand tag={Link} to="/">CapitalData</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} to="/">Home</NavLink>
                                </NavItem>
                                <NavItem hidden={this.state.authenticated}>
                                    <NavLink tag={Link} to="/sign-in">Sign in</NavLink>
                                </NavItem>
                                <NavItem hidden={!this.state.authenticated}>
                                    <NavLink tag={Link} to="/profile">Profile</NavLink>
                                </NavItem>
                                <NavItem hidden={!this.state.authenticated}>
                                    <NavLink tag={Link} to="/sign-out">Sign out</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
