import config from 'config'
import {
  authHeader, refreshToken
} from './auth-header'
import history from './history'

function fetchLastRequest(requestBody) {
  return fetch(config.apiUrl.SERVER_URL + '/graphql', {
    method: 'POST',
    body: requestBody,
    headers: authHeader()
  })
}

async function fetchRefreshToken(response, lastRequestBody) {
  let cloneRes = response.clone()
  return cloneRes.text().then(text => {
    const data = text && JSON.parse(text)
    if (data[0] === 'Token expired') {

      let requestBody = {
        query: `query { 
                refreshToken(refreshToken:"${refreshToken()}") {
                    token
                    refreshToken  
                }}`
      }
      return fetch(config.apiUrl.SERVER_URL + '/graphql', {
        method: 'POST',
        body: JSON.stringify(requestBody),
        headers: authHeader()
      })
        .then(handleResponse)
        .then(tokens => {
          let user = JSON.parse(localStorage.getItem('user'))

          const data = {
            token: tokens.data.refreshToken.token,
            refreshToken: tokens.data.refreshToken.refreshToken,
            login: user.login
          }
          localStorage.setItem('user', JSON.stringify(data))
        })
        .then(() => {
          return fetchLastRequest(lastRequestBody)
        })
    } else {
      return response
    }
  })
}

export function fetchR(requestBody) {
  return fetch(config.apiUrl.SERVER_URL + '/graphql', {
    method: 'POST',
    body: requestBody,
    headers: authHeader()
  }).then((res) => { return fetchRefreshToken(res, requestBody) })
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text)
    if (!response.ok) {
      if (response.status === 400) {
        if (data[0] === 'Invalid refresh token') {
          localStorage.removeItem('user')
          history.push('/')
        }
      }

      const error = (data && data.message) || response.statusText
      return Promise.reject(error)
    }

    return data
  })
}