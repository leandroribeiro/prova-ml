export class User {
  constructor(username: string, password: string, fakeJwtToken: string) {
    this.username = username;
    this.password = password;
    this.token = fakeJwtToken;
  }

  username: string;
  password: string;
  token?: string;
}
