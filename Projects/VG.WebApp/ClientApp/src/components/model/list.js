import React, { Component } from "react";
import ModelDataService from "../../services/model.services";

import { Link } from "react-router-dom";

export default class List extends Component {
  constructor(props) {
    super(props);

    this.retrieveModels = this.retrieveModels.bind(this);
    this.refreshList = this.refreshList.bind(this);
    this.onClickDeleteModel = this.onClickDeleteModel.bind(this);

    this.state = {
      Models: [],
      currentModel: null,
      currentIndex: -1,
      erroMessage: null,
    };
  }

  componentDidMount() {
    this.retrieveModels();
  }

  setActive(Model, index) {
    this.setState({
      currentModel: Model,
      currentIndex: index,
    });
  }

  retrieveModels() {
    ModelDataService.getAll()
      .then((response) => {
        this.setState({
          Models: response.data,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  refreshList() {
    this.retrieveModels();
    this.setState({
      currentModel: null,
      currentIndex: -1,
    });
  }

  onClickDeleteModel(id, e) {
    ModelDataService.delete(id)
      .then((response) => {
        console.log(response.data);
        this.refreshList();
      })
      .catch((e) => {
        this.setState({ erroMessage: e.response.data });
      });
  }

  render() {
    const { Models, erroMessage } = this.state;

    return (
      <div className="row">
        <div className="col-md-10">
          <h4>
            Models List
            <Link
              to={"/Model/add"}
              className="btn btn-primary btn-sm float-right"
            >
              Add New
            </Link>
          </h4>

          {erroMessage && (
            <div className="alert alert-danger" role="alert">
              {erroMessage}
            </div>
          )}

          <table className="table table-hover">
            <thead className="thead-dark">
              <tr>
                <th scope="col">#</th>
                <th scope="col">Name</th>
                <th scope="col"></th>
              </tr>
            </thead>
            <tbody>
              {Models &&
                Models.map((Model, index) => (
                  <tr key={index}>
                    <th scope="row">{Model.id}</th>
                    <td>{Model.name}</td>
                    <td>
                      <Link
                        to={"/Model/" + Model.id}
                        className="btn btn-light btn-sm"
                      >
                        Edit
                      </Link>
                      <button
                        className="btn btn-danger btn-sm"
                        onClick={(e) => this.onClickDeleteModel(Model.id, e)}
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
