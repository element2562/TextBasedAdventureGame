import React from "react";
import Api from "../ApiCalls.js";
import { Table } from "react-bootstrap";

export default class Players extends React.Component
{
    state = {
        players: []
    }

    componentDidMount()
    {
        Api.GetAllPlayers()
        .then(res => {
            this.setState({
                players: res
            })
        })
    }

    render()
    {
        return(
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Player Name</th>
                        <th>Level</th>
                        <th>Health</th>
                        <th>Max Health</th>
                        <th>Strength</th>
                        <th>Defense</th>
                        <th>Gold</th>
                        <th>Experience</th>
                        <th>Current zone</th>
                        <th>Edit</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.players.map(player => (
                        <tr key={player.playerId}>
                            <th>{player.playerName}</th>
                            <th>{player.level}</th>
                            <th>{player.health}</th>
                            <th>{player.maxHealth}</th>
                            <th>{player.strength}</th>
                            <th>{player.defense}</th>
                            <th>{player.gold}</th>
                            <th>{player.experience}</th>
                            <th>{player.currentZone?.zoneName || "-"}</th>
                            <th><a href="/">Edit</a></th>
                        </tr>
                    ))}
                </tbody>
            </Table>
        )
    }
}