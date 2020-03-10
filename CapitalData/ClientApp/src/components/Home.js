import React, { Component } from 'react';
import './Home.css'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>


            <div id="masthead" className="position-relative overflow-hidden px-md-5 text-center bg-light">
                <div className="col-md-5 p-lg-5 mx-auto mt-md-0 mt-2 mb-5">
                    <h1 className="display-4 font-weight-light">
                        Capital Data
        </h1>
                    <p className="lead font-weight-light">
                        Legislative data from the House of Representatives, the Senate and the Library of Congress at your fingertips.
        </p>
                    <a className="btn btn-outline-dark" href="#">Create account</a>
                    <a className="btn btn-outline-secondary" href="#">Sign in</a>
                </div>
            </div>

            <section>
                <div>
                    <div className="row no-gutters bg-light">
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Members">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="users"></span>
                                            Members
                            </h2>
                                        <p className="lead text-muted">Member data is available for every member who has served in Congress.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Bills">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="file-text"></span>
                                            Bills
                            </h2>
                                        <p className="lead text-muted">Bill data is available by subject, voting statistics and more.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Votes">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="thumbs-up"></span>
                                            Votes
                            </h2>
                                        <p className="lead text-muted">Basic vote data along with additional useful information.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Statements">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="message-circle"></span>
                                            Statements
                            </h2>
                                        <p className="lead text-muted">Data about press releases published by memebers of Congress.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Committees">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="bookmark"></span>
                                            Committees
                            </h2>
                                        <p className="lead text-muted">Data about Congressional Committee memberships.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Nominations">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="pen-tool"></span>
                                            Nominations
                            </h2>
                                        <p className="lead text-muted">Data about presidential civilian nomination lists and details.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Lobbying">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="activity"></span>
                                            Lobbying
                            </h2>
                                        <p className="lead text-muted">Data about filling from registered lobbyists.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div className="col-md-3">
                            <a className="text-decoration-none" href="/Other">
                                <div className="card h-100">
                                    <div className="card-body">
                                        <h2 className="display-5 text-dark font-weight-light">
                                            <span data-feather="archive"></span>
                                            Other
                            </h2>
                                        <p className="lead text-muted">Various supplemental information is also available.</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </section>

            <section className="cta">
                <div className="cta-content">
                    <div className="container">
                        <h2 className="font-weight-light">
                            It's not rocket science.
            </h2>
                        <a href="#contact" className="btn btn-outline-light">Learn more about Capital Data</a>
                    </div>
                </div>
                <div className="overlay"></div>
            </section>
      </div>
    );
  }
}
