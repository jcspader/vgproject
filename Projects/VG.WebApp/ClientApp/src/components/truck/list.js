import React, { Component } from "react";
import TruckDataService from "../../services/truck.services";

import { Link } from "react-router-dom";

export default class List extends Component {
  constructor(props) {
    super(props);

    this.retrieveTrucks = this.retrieveTrucks.bind(this);
    this.refreshList = this.refreshList.bind(this);
    this.onClickDeleteTruck = this.onClickDeleteTruck.bind(this);

    this.state = {
      Trucks: [],
      currentTruck: null,
      currentIndex: -1,
    };
  }

  componentDidMount() {
    this.retrieveTrucks();
  }

  setActive(truck, index) {
    this.setState({
      currentTruck: truck,
      currentIndex: index,
    });
  }

  retrieveTrucks() {
    TruckDataService.getAll()
      .then((response) => {
        this.setState({
          Trucks: response.data,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  refreshList() {
    this.retrieveTrucks();
    this.setState({
      currentTruck: null,
      currentIndex: -1,
    });
  }

  onClickDeleteTruck(id, e) {
    TruckDataService.delete(id)
      .then((response) => {
        console.log(response.data);
        this.refreshList();
      })
      .catch((e) => {
        console.log(e);
      });
  }

  render() {
    const { Trucks } = this.state;

    return (
      <div className="row">
        <div className="col-md-10">
          <h4>
            Trucks List
            <Link
              to={"truck/add"}
              className="btn btn-primary btn-sm float-right"
            >
              Add New
            </Link>
          </h4>

          <table className="table table-hover">
            <thead className="thead-dark">
              <tr>
                <th scope="col">#</th>
                <th scope="col">Model</th>
                <th scope="col">Year</th>
                <th scope="col">Color</th>
                <th scope="col">Price</th>
                <th scope="col"></th>
              </tr>
            </thead>
            <tbody>
              {Trucks &&
                Trucks.map((Truck, index) => (
                  <tr key={index}>
                    <th scope="row">{Truck.id}</th>
                    <td>{Truck.modelName}</td>
                    <td>
                      {Truck.manufactureYear} / {Truck.modelYear}
                    </td>
                    <td>{Truck.color}</td>
                    <td>{Truck.price}</td>
                    <td>
                      <Link
                        to={"/Truck/" + Truck.id}
                        className="btn btn-light btn-sm"
                      >
                        Edit
                      </Link>{" "}
                      <button
                        className="btn btn-danger btn-sm"
                        onClick={(e) => this.onClickDeleteTruck(Truck.id, e)}
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>
        </div>
      </div>
    );
  }
}
