import React, { Component } from 'react';
import { Authentication } from '../../services/authentication'

export class SignOut extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        this.signOut()
    }

    signOut = () => {
        Authentication.clearLocalStorage();
        window.location.href = '/'
    }

    render() {
        return (
            <div>
                <h1>Signing you out..</h1>
            </div>
            )
    }
}