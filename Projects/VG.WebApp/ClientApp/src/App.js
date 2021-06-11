import React, { Component } from "react";
import { Switch, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import TruckAdd from "./components/truck/add";
import TruckEdit from "./components/truck/edit";
import TruckList from "./components/truck/list";

class App extends Component {
  render() {
    return (
      <div>
        <nav className="navbar navbar-expand navbar-dark bg-dark">
          <Link to={"/trucks"} className="navbar-brand">
            Volvo Group
          </Link>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/trucks"} className="nav-link">
                Trucks
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"truck/add"} className="nav-link">
                Add
              </Link>
            </li>
          </div>
        </nav>

        <div className="container mt-3">
          <Switch>
            <Route exact path={["/", "/trucks"]} component={TruckList} />
            <Route exact path="/truck/add" component={TruckAdd} />
            <Route path="/truck/:id" component={TruckEdit} />
          </Switch>
        </div>
      </div>
    );
  }
}

export default App;
