import axios from "axios";
import Chat from "./chat";
// import notice from '../components/Notice'
// import message from '../components/SessionExpiration'

axios.defaults.baseURL =
  process.env.NODE_ENV === "development"
    ? process.env.SERVER_API_URL
    : window.location.origin + "/api";
axios.defaults.headers["Pragma"] = "no-cache";
axios.defaults.headers["Cache-Control"] = "no-cache, no-store";
axios.interceptors.request.use(
  (config) => {
    const language = window.localStorage.getItem("language");

    if (language) {
      config.headers["Accept-Language"] = language;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.request.use(
  (config) => {
    const jsonUser = window.localStorage.getItem("user");

    if (jsonUser) {
      let user = JSON.parse(jsonUser);
      config.headers.Authorization = `Bearer ${user.token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  const originalRequest = error.config;
  if (error.response.status === 401 && !originalRequest._retry) {
    originalRequest._retry = true;
    const jsonUser = window.localStorage.getItem("user");
    if (jsonUser) {
      let user = JSON.parse(jsonUser);
      return axios
        .post("/account/refresh", {
          token: user.token,
          //refreshToken: user.refreshToken,
          sessionId: user.sessionId,
        })
        .then((res) => {
          if (res.status === 200) {
            window.localStorage.setItem(
              "user",
              JSON.stringify({
                userName: res.data.userName,
                token: res.data.token,
                refreshToken: res.data.refreshToken,
                sessionId: res.data.sessionId,
              })
            );
            if (originalRequest.url.includes("user/logout")) {
              return User.logout({
                //refreshToken: res.data.refreshToken,
                sessionId: res.data.sessionId,
              });
            } else {
              return axios(originalRequest);
            }
          } else {
            return Promise.reject(error.response);
          }
        })
        .catch((err) => {
          return Promise.reject(err);
        });
    } else if (!originalRequest.url.includes("refreshUserData")) null;
  } else if (
    error.response.status == 403 &&
    error.response.data.errors == "Invalid RefreshToken"
  ) {
    const jsonUser = window.localStorage.getItem("user");
    if (jsonUser) null;
    // notice(
    //   "error",
    //   "Ошибка аутентификации",
    //   "Время сессии истекло. Пожалуйста, повторите вход в личный кабинет"
    // );
  } else {
    return Promise.reject(error.response);
  }
});

const responseBody = (response) => response.data;
export const requests = {
  getFile: (url, query = {}) =>
    axios
      .get(url, {
        responseType: "blob",
        params: query,
      })
      .then(responseBody),
  get: (url) => axios.get(url).then(responseBody),
  getWithParams: (url, query = {}) =>
    axios
      .get(url, {
        params: query,
      })
      .then(responseBody),
  post: (url, body = {}) => axios.post(url, body).then(responseBody),
  put: (url, body = {}) => axios.put(url, body).then(responseBody),
  del: (url, query = {}) =>
    axios
      .delete(url, {
        data: query,
      })
      .then(responseBody),
  postForm: (url, file) => {
    let formData = new FormData();
    formData.append("File", file);
    return axios
      .post(url, formData, {
        headers: {
          "Content-type": "multipart/form-data",
        },
      })
      .then(responseBody);
  },
  postFormMany: (url, files) => {
    const fd = new FormData();
    files.forEach((file) => {
      fd.append("Files", file);
    });
    return axios
      .post(url, fd, {
        headers: {
          "Content-type": "multipart/form-data",
        },
      })
      .then(responseBody);

    // var results = []
    // files.forEach(file => {
    //   results.push(postForm(url, file))
    // })
    // return results
  },
};

const User = {
  login: (user) => requests.post("/user/login", user),
  belpochtaLogin: (data) =>
    requests.post("/externalLogin/belpochtaLogin", data),
  signlogin: (pki) => requests.post("/user/signlogin", pki),
  changeSign: (data) => requests.post("/user/changesign", data),
  logout: (data) => requests.post("/user/logout", data),
  refreshUserData: () => requests.get("/user/refreshUserData"),
  confirmPhone: (data) => requests.post("/phone/confirmMobilePhone", data),
  getUserTypes: (data) => requests.post("/user/getUserTypes", data),
  getRoles: (data) => requests.get("/user/getRoles"),
  getFunctions: (data) => requests.get("/user/getFunctions"),
  updateRoleFunctions: (data) =>
    requests.post("/user/updateRoleFunctions", data),

  //register;
  preChecking: (user) => requests.post("/user/RegistrationPreCheking", user),
  registration: (user) => requests.post("/user/Registration", user),
  //unblock;
  unblock: (data) => requests.post("/unblock/unblock", data),
  unblockPreCheking: (data) => requests.post("/unblock/preChecking", data),
};

export default {
  User,
  requests,
  Chat,
};
