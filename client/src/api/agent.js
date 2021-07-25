import axios from 'axios'
import notice from '../components/Notice'
import message from '../components/SessionExpiration'

axios.defaults.baseURL =
  process.env.NODE_ENV === 'development'
    ? process.env.SERVER_API_URL
    : window.location.origin + '/api'
axios.defaults.headers['Pragma'] = 'no-cache'
axios.defaults.headers['Cache-Control'] = 'no-cache, no-store'
axios.interceptors.request.use(
  (config) => {
    const language = window.localStorage.getItem('language')

    if (language) {
      config.headers['Accept-Language'] = language
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

axios.interceptors.request.use(
  (config) => {
    const jsonUser = window.localStorage.getItem('user')

    if (jsonUser) {
      let user = JSON.parse(jsonUser)
      config.headers.Authorization = `Bearer ${user.token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

axios.interceptors.response.use(undefined, (error) => {
  const originalRequest = error.config
  if (error.response.status === 401 && !originalRequest._retry) {
    originalRequest._retry = true
    const jsonUser = window.localStorage.getItem('user')
    if (jsonUser) {
      let user = JSON.parse(jsonUser)
      return axios
        .post('/account/refresh', {
          token: user.token,
          //refreshToken: user.refreshToken,
          sessionId: user.sessionId,
        })
        .then((res) => {
          if (res.status === 200) {
            window.localStorage.setItem(
              'user',
              JSON.stringify({
                userName: res.data.userName,
                token: res.data.token,
                refreshToken: res.data.refreshToken,
                sessionId: res.data.sessionId,
              })
            )
            if (originalRequest.url.includes('user/logout')) {
              return User.logout({
                //refreshToken: res.data.refreshToken,
                sessionId: res.data.sessionId,
              })
            } else {
              return axios(originalRequest)
            }
          } else {
            return Promise.reject(error.response)
          }
        })
        .catch((err) => {
          return Promise.reject(err)
        })
    } else if (!originalRequest.url.includes('refreshUserData')) message()
  } else if (
    error.response.status == 403 &&
    error.response.data.errors == 'Invalid RefreshToken'
  ) {
    const jsonUser = window.localStorage.getItem('user')
    if (jsonUser) message()
    // notice(
    //   "error",
    //   "Ошибка аутентификации",
    //   "Время сессии истекло. Пожалуйста, повторите вход в личный кабинет"
    // );
  } else {
    return Promise.reject(error.response)
  }
})

const responseBody = (response) => response.data
export const requests = {
  getFile: (url, query = {}) =>
    axios
      .get(url, {
        responseType: 'blob',
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
    let formData = new FormData()
    formData.append('File', file)
    return axios
      .post(url, formData, {
        headers: {
          'Content-type': 'multipart/form-data',
        },
      })
      .then(responseBody)
  },
  postFormMany: (url, files) => {
    const fd = new FormData()
    files.forEach((file) => {
      fd.append('Files', file)
    })
    return axios
      .post(url, fd, {
        headers: {
          'Content-type': 'multipart/form-data',
        },
      })
      .then(responseBody)

    // var results = []
    // files.forEach(file => {
    //   results.push(postForm(url, file))
    // })
    // return results
  },
}

const User = {
  login: (user) => requests.post('/account/login', user),
  belpochtaLogin: (data) => requests.post('/externalLogin/belpochtaLogin', data),
  signlogin: (pki) => requests.post('/account/signlogin', pki),
  changeSign: (data) => requests.post('/account/changesign', data),
  logout: (data) => requests.post('/account/logout', data),
  refreshUserData: () => requests.get('/account/refreshUserData'),
  confirmPhone: (data) => requests.post('/phone/confirmMobilePhone', data),
  getUserTypes: (data) => requests.post('/account/getUserTypes', data),
  getRoles: (data) => requests.post('/account/getRoles', data),
  //register;
  preChecking: (user) => requests.post('/account/preChecking', user),
  citizenRegister: (user) => requests.post('/account/citizenRegister', user),
  foreignRegister: (user) => requests.post('/account/foreignRegister', user),
  statelessRegister: (user) =>
    requests.post('/account/statelessRegister', user),
  individualRegister: (user) =>
    requests.post('/account/individualRegister', user),
  juridicalRegister: (user) =>
    requests.post('/account/juridicalRegister', user),
  foreignCompanyRegister: (user) =>
    requests.post('/account/foreignCompanyRegister', user),
  //change;
  citizenChange: (user) => requests.post('/account/citizenChange', user),
  foreignChange: (user) => requests.post('/account/foreignChange', user),
  statelessChange: (user) => requests.post('/account/statelessChange', user),
  individualChange: (user) => requests.post('/account/individualChange', user),
  juridicalChange: (user) => requests.post('/account/juridicalChange', user),
  foreignCompanyChange: (user) =>
    requests.post('/account/foreignCompanyChange', user),
  //unblock;
  unblock: (data) => requests.post('/unblock/unblock', data),
  unblockPreCheking: (data) => requests.post('/unblock/preChecking', data),
}

export default {
  User,
  requests,
}
