import React, { useState } from "react";
import { Form, FormGroup, Label, Input } from "reactstrap";

const ChangePassword = () => {
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmNewPassword, setConfirmNewPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();

    await fetch("http://localhost:7277/api/account/password/change", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      credentials: "include",
      body: JSON.stringify({
        login: "ADMINEK",
        oldPassword: oldPassword,
        password: newPassword,
        confirmPassword: confirmNewPassword,
      }),
    })
      .then((response) => {
        console.log(response.status);
        return response.json();
      })
      .then((res) => {
        window.location.href = "/";
      })
      .catch((error) => console.log(error));
  };

  return (
    <Form onSubmit={handleSubmit}>
      <FormGroup>
        <Label for="oldPassword">Old password:</Label>
        <Input
          id="oldPassword"
          type="text"
          value={oldPassword}
          size="lg"
          onChange={(event) => setOldPassword(event.target.value)}
        />
      </FormGroup>

      <FormGroup>
        <Label for="newPassword">New password:</Label>
        <Input
          id="newPassword"
          type="text"
          value={newPassword}
          size="lg"
          onChange={(event) => setNewPassword(event.target.value)}
        />
      </FormGroup>

      <FormGroup>
        <Label for="confirmNewPassword">Confirm new password:</Label>
        <Input
          id="confirmNewPassword"
          type="text"
          value={confirmNewPassword}
          size="lg"
          onChange={(event) => setConfirmNewPassword(event.target.value)}
        />
      </FormGroup>

      <FormGroup>{error}</FormGroup>
      <button className="btn btn-secondary" type="submit">
        Change password
      </button>
    </Form>
  );
};

export default ChangePassword;
