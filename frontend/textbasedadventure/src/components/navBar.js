import React from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";

const navBar = () => {
    return(
        <Navbar bg="light" expand="lg">
            <Navbar.Brand href="/">Text Based Adventure</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="mr-auto">
                <NavDropdown title="Tables">
                    <NavDropdown.Item href="players">Players</NavDropdown.Item>
                    <NavDropdown.Item href="zones">Zones</NavDropdown.Item>
                    <NavDropdown.Item href="items">Items</NavDropdown.Item>
                    <NavDropdown.Item href="monsters">Monsters</NavDropdown.Item>
                    <NavDropdown.Item href="events">Events</NavDropdown.Item>
                    <NavDropdown.Item href="quests">Quests</NavDropdown.Item>
                    <NavDropdown.Item href="npcs">NPCs</NavDropdown.Item>                    
                </NavDropdown>
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    )
}
export default navBar;