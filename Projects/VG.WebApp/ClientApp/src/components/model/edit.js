import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";

import ModelDataService from "../../services/model.services";

export default function Edit(props) {
  const [submitted, setSubmitted] = useState(false);
  const [erroMessage, setErroMessage] = useState();

  const {
    register,
    handleSubmit,
    setValue,
    reset,
    formState: { errors },
  } = useForm();

  useEffect(() => {
    ModelDataService.get(props.match.params.id)
      .then((response) => {
        setTimeout(() => {
          reset(response.data);
        }, 500);
      })
      .catch((e) => {
        setErroMessage(e.response.data);
      });
  }, [props.match.params.id, reset, setValue]);

  const onUpdateModel = (data) => {
    console.warn(data);

    ModelDataService.update(props.match.params.id, data)
      .then((response) => {
        console.log(response.data);
        setSubmitted(true);
      })
      .catch((e) => {
        setErroMessage(e.response.data);
      });
  };

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>The Model was updated successfully!</h4>
          <Link to={"/Models/"} className="btn btn-light btn-sm">
            Return to List
          </Link>
        </div>
      ) : (
        <div>
          <h4>Update Model</h4>

          {erroMessage && (
            <div className="alert alert-danger" role="alert">
              {erroMessage}
            </div>
          )}

          <form onSubmit={handleSubmit(onUpdateModel)}>
            <div className="form-group">
              <label htmlFor="color">Name</label>
              <input
                type="text"
                className={`form-control ${errors.name ? "is-invalid" : ""}`}
                {...register("name", { maxLength: 15 })}
              />
              {errors.name && (
                <div className="invalid-feedback">
                  {errors.name?.type === "maxLength" &&
                    "The max length is 15 characters"}
                </div>
              )}
            </div>

            <button type="submit" className="btn btn-success">
              Update
            </button>
          </form>
        </div>
      )}
    </div>
  );
}
