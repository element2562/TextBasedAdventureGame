import React from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import NavBar from "./components/navBar.js";
import HomePage from "./components/HomePage.js";
import Players from "./components/Players.js";
class Views extends React.Component {
    render()
    {
        return(
            <Router>
                <Route path="/" component={() => <NavBar /> } />
                <Route exact path="/" component={() => <HomePage /> } />
                <Route exact path="/players" component={() => <Players />} />
            </Router>
        )
    }
}
export default Views;