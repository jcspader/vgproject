import React, { Component } from "react";
import TruckDataService from "../../services/truck.services";
import ModelDataService from "../../services/model.services";

export default class Add extends Component {
  constructor(props) {
    super(props);

    this.onChangeModel = this.onChangeModel.bind(this);
    this.onChangeManufactureYear = this.onChangeManufactureYear.bind(this);
    this.onChangeModelYear = this.onChangeModelYear.bind(this);
    this.onChangeColor = this.onChangeColor.bind(this);
    this.onChangePrice = this.onChangePrice.bind(this);

    this.saveTruck = this.saveTruck.bind(this);
    this.newTruck = this.newTruck.bind(this);

    this.state = {
      manufactureYear: 2021,
      modelYear: 2022,
      color: "",
      price: 0,
      modelId: 0,

      submitted: false,
    };
  }

  onChangeModel(e) {
    this.setState({
      modelId: e.target.value,
    });
  }
  onChangeManufactureYear(e) {
    this.setState({
      manufactureYear: e.target.value,
    });
  }
  onChangeModelYear(e) {
    this.setState({
      modelYear: e.target.value,
    });
  }
  onChangeColor(e) {
    this.setState({
      color: e.target.value,
    });
  }
  onChangePrice(e) {
    this.setState({
      price: e.target.value,
    });
  }

  saveTruck() {
    var data = {
      modelId: this.state.modelId,
      manufactureYear: this.state.manufactureYear,
      modelYear: this.state.modelYear,
      color: this.state.color,
      price: this.state.price,
    };

    TruckDataService.create(data)
      .then((response) => {
        this.setState({
          id: response.data.id,
          title: response.data.title,
          description: response.data.description,
          published: response.data.published,

          submitted: true,
        });
      })
      .catch((e) => {
        console.log(e);
      });
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

  newTruck() {
    this.setState({
      manufactureYear: 2021,
      modelYear: 2022,
      color: "",
      price: 0,
      modelId: 0,

      submitted: false,
    });
  }

  componentDidMount() {
    this.retrieveModels();
  }

  render() {
    const { Models } = this.state;

    return (
      <div className="submit-form">
        {this.state.submitted ? (
          <div>
            <h4>You submitted successfully!</h4>
            <button className="btn btn-success" onClick={this.newTruck}>
              Add New
            </button>
          </div>
        ) : (
          <div>
            <h4>Create a new Truck</h4>

            <div className="form-group">
              <label htmlFor="modelId">Model</label>
              <select
                className="form-control"
                value={this.state.modelId}
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
                    value={this.state.manufactureYear}
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
                    value={this.state.modelYear}
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
                value={this.state.color}
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
                  value={this.state.price}
                  onChange={this.onChangePrice}
                  name="price"
                />
              </div>
            </div>

            <button onClick={this.saveTruck} className="btn btn-success">
              Create
            </button>
          </div>
        )}
      </div>
    );
  }
}
