import React, { Component } from 'react';

export class MemberList extends Component {
    constructor(props) {
        super(props)
        this.state = {
            members: [],
            loading: true
        }
        this.fetchMembers()
    }
    fetchMembers() {
        fetch('congress/members')
            .then(response => response.json())
            .then(data => this.setState({ members: data, loading: false }))
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading Members...</em></p>
            : MemberList.renderMemberList(this.state.members);
        return (
            <div className="container">
                <h1>
                    Members
                </h1>
                {contents}
            </div>
            )
    }
    static renderMemberList(members) {
        return (
            <table className="table table-hover table-sm">
                <thead className="thead-dark">
                    <tr>
                        <th>Name</th>
                        <th>Title</th>
                        <th>Votes With Party</th>
                        <th>Missed Votes</th>
                    </tr>
                </thead>
                <tbody>
                    {members.map(m =>
                        <tr key={m.id}>
                            <td>
                                ({m.party}) {m.first_name} {m.last_name}
                            </td>
                            <td>
                                {m.title}
                            </td>
                            <td>
                                {m.votes_with_party_pct}%
                            </td>
                            <td>
                                {m.missed_votes_pct}%
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
            )
    }
}