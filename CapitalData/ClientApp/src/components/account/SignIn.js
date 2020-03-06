import React, { Component } from 'react';
import { Authentication } from '../../services/authentication'

export class SignIn extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: ''
        };
    }

    myChangeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    requestSignIn = (event) => {
        event.preventDefault();
        var user = {
            username: this.state.username,
            password: this.state.password
        };
        this.trySignIn(user);
    }

    async trySignIn(user) {
        const response = await fetch('account/signin', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        const data = await response.json();
        if (data && data.id > 0) {
            Authentication.setUserId(data.id)
            //redirect to profile
            //this.props.history.push('/profile')
            window.location.href = '/profile'
        }
        else {
            alert('The username or password was incorrect.')
        }
    }

    render() {
        return (
            <div>
                <h1>Sign In</h1>
                <form method="post" onSubmit={this.requestSignIn}>
                    <div className="form-group">
                        <label htmlFor="username">Username</label>
                        <input type="text" name="username" id="username" className="form-control" onChange={this.myChangeHandler} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input type="password" name="password" id="password" className="form-control" onChange={this.myChangeHandler} />
                    </div>
                    <button className="btn btn-primary" type="submit">Sign in</button>
                </form>
            </div>
            )
    }
}
