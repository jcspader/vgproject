import React, { Component } from "react";
import { Link } from "react-router-dom";

import TruckDataService from "../../services/truck.services";
import ModelDataService from "../../services/model.services";

export default class Edit extends Component {
  constructor(props) {
    super(props);
    this.getTruck = this.getTruck.bind(this);
    this.updateTruck = this.updateTruck.bind(this);
    this.onChangeModel = this.onChangeModel.bind(this);
    this.onChangeManufactureYear = this.onChangeManufactureYear.bind(this);
    this.onChangeModelYear = this.onChangeModelYear.bind(this);
    this.onChangeColor = this.onChangeColor.bind(this);
    this.onChangePrice = this.onChangePrice.bind(this);

    this.state = {
      currentTruck: {
        manufactureYear: 0,
        modelYear: 0,
        color: "",
        price: 0,
        modelId: 0,
      },
      message: "",
      submitted: false,
    };
  }

  componentDidMount() {
    this.retrieveModels();
    this.getTruck(this.props.match.params.id);
  }

  retrieveModels() {
    ModelDataService.getAll()
      .then((response) => {
        this.setState({
          Models: response.data,
        });
      })
      .catch((e) => {
        console.log(e);
      });
  }

  onChangeModel(e) {
    const value = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        modelId: value,
      },
    }));
  }
  onChangeManufactureYear(e) {
    const value = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        manufactureYear: value,
      },
    }));
  }
  onChangeModelYear(e) {
    const value = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        modelYear: value,
      },
    }));
  }
  onChangeColor(e) {
    const value = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        color: value,
      },
    }));
  }
  onChangePrice(e) {
    const value = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        price: value,
      },
    }));
  }

  onChangeDescription(e) {
    const description = e.target.value;

    this.setState((prevState) => ({
      currentTruck: {
        ...prevState.currentTruck,
        description: description,
      },
    }));
  }

  getTruck(id) {
    TruckDataService.get(id)
      .then((response) => {
        this.setState({
          currentTruck: response.data,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  updateTruck() {
    TruckDataService.update(this.state.currentTruck.id, this.state.currentTruck)
      .then((response) => {
        console.log(response.data);
        this.setState({
          message: "The Truck was updated successfully!",
          submitted: true,
        });
      })
      .catch((e) => {
        console.log(e);
      });
  }

  deleteTruck() {
    TruckDataService.delete(this.state.currentTruck.id)
      .then((response) => {
        console.log(response.data);
        this.props.history.push("/Trucks");
      })
      .catch((e) => {
        console.log(e);
      });
  }

  render() {
    const { Models, message } = this.state;

    return (
      <div className="submit-form">
        {this.state.submitted ? (
          <div>
            <h4>{message}</h4>
            <Link to={"/Trucks/"} className="btn btn-light btn-sm">
              Return to List
            </Link>
          </div>
        ) : (
          <div>
            <h4>Update Truck</h4>

            <div className="form-group">
              <label htmlFor="modelId">Model</label>
              <select
                className="form-control"
                value={this.state.currentTruck.modelId}
                onChange={this.onChangeModel}
              >
                <option disabled value="0">
                  Models
                </option>
                {Models &&
                  Models.map((Model, index) => (
                    <option key={Model.id} value={Model.id}>
                      {Model.name}
                    </option>
                  ))}
              </select>
            </div>

            <div className="form-group">
              <div className="row">
                <div className="col">
                  <label htmlFor="manufactureYear">Manufacture Year</label>
                  <input
                    type="number"
                    className="form-control"
                    id="manufactureYear"
                    required
                    value={this.state.currentTruck.manufactureYear}
                    onChange={this.onChangeManufactureYear}
                    name="manufactureYear"
                  />
                </div>
                <div className="col">
                  <label htmlFor="modelYear">Model Year</label>
                  <input
                    type="number"
                    className="form-control"
                    id="modelYear"
                    required
                    value={this.state.currentTruck.modelYear}
                    onChange={this.onChangeModelYear}
                    name="modelYear"
                  />
                </div>
              </div>
            </div>

            <div className="form-group">
              <label htmlFor="color">Color</label>
              <input
                type="text"
                className="form-control"
                id="color"
                required
                value={this.state.currentTruck.color}
                onChange={this.onChangeColor}
                name="color"
              />
            </div>

            <div className="form-group">
              <label htmlFor="price">Price</label>
              <div className="input-group">
                <div className="input-group-prepend">
                  <div className="input-group-text">R$</div>
                </div>
                <input
                  type="text"
                  className="form-control"
                  id="price"
                  required
                  value={this.state.currentTruck.price}
                  onChange={this.onChangePrice}
                  name="price"
                />
              </div>
            </div>

            <button onClick={this.updateTruck} className="btn btn-success">
              Update
            </button>
          </div>
        )}
      </div>
    );
  }
}
