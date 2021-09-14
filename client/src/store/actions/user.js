import {
  userConstants 
} from '../constants/'
import {
  defAction 
} from '../../helpers/defaultAction'
import agent from '../../api/agent'
import history from '../../helpers/history'

export const userActions = {
  login,
  belpochtaLogin,
  signlogin,
  changesign,
  logout,
  refreshUserData,
  unblock,
  register,
  getUserTypes,
  changeProfileData,
  confirmPhone,
  getRoles,
  registerByAdmin,
  registerByExternalAdmin,
}

function login(values) {
  const dispatchObj = {
    constants: userConstants.Login,
    service: {
      func: agent.User.login,
      params: values,
    },
    sucFunction: () => history.push('/account'),
  }
  return defAction(dispatchObj)
}

function belpochtaLogin(values) {
  const dispatchObj = {
    constants: userConstants.BelpochtaLogin,
    service: {
      func: agent.User.belpochtaLogin,
      params: values,
    },
    sucFunction: () => history.push('/account'),
  }
  return defAction(dispatchObj)
}

function signlogin(pki) {
  const dispatchObj = {
    constants: userConstants.SignLogin,
    service: {
      func: agent.User.signlogin,
      params: {
        pki 
      },
    },
    sucFunction: () => history.push('/account'),
  }
  return defAction(dispatchObj)
}

function changesign(values) {
  const dispatchObj = {
    constants: userConstants.ChangeSign,
    service: {
      func: agent.User.changeSign,
      params: values,
    },
  }
  return defAction(dispatchObj)
}

function logout() {
  let jsonUser = window.localStorage.getItem('user')
  //let refreshToken = JSON.parse(jsonUser).refreshToken;
  let sessionId = JSON.parse(jsonUser).sessionId
  const dispatchObj = {
    constants: userConstants.Logout,
    sucFunction: () => {
      history.push('/')
    },
    service: {
      func: agent.User.logout,
      params: {
        //refreshToken: refreshToken,
        sessionId: sessionId,
      },
    },
    // sucFunction: () => history.push("/"),
  }
  return defAction(dispatchObj)
}

function refreshUserData() {
  const dispatchObj = {
    constants: userConstants.RefreshUserData,
    service: {
      func: agent.User.refreshUserData,
    },
  }
  return defAction(dispatchObj)
}

function unblock(values) {
  const dispatchObj = {
    constants: userConstants.Unblock,
    service: {
      func: agent.User.unblock,
      params: values,
    },
  }
  return defAction(dispatchObj)
}

function confirmPhone(values) {
  const dispatchObj = {
    constants: userConstants.ConfirmPhone,
    service: {
      func: agent.User.confirmPhone,
      params: values,
    },
    sucFunction: () => history.push('/login'),
  }
  return defAction(dispatchObj)
}

function register(values) {
  const dispatchObj = {
    constants: userConstants.Register,
    service: {
      func: getAgentUserFunction(values.userType.name, 'Register'),
      params: values,
    },
    sucFunction: () => history.push('/login'),
  }
  return defAction(dispatchObj)
}

function registerByAdmin(values) {
  const dispatchObj = {
    constants: userConstants.RegisterByAdmin,
    service: {
      func: getAgentUserFunction(values.userType.name, 'Register'),
      params: values,
    },
    sucFunction: () => history.push('/users'),
  }
  return defAction(dispatchObj)
}

function registerByExternalAdmin(values) {
  const dispatchObj = {
    constants: userConstants.RegisterByExternalAdmin,
    service: {
      func: getAgentUserFunction(values.userType.name, 'Register'),
      params: values,
    },
    sucFunction: () => history.push('/orgUsers'),
  }
  return defAction(dispatchObj)
}

function getAgentUserFunction(userTypeName, type) {
  switch (userTypeName) {
    case 'foreign':
      return agent.User[userTypeName + type]
    case 'citizen':
      return agent.User[userTypeName + type]
    case 'stateless':
      return agent.User[userTypeName + type]
    case 'juridical':
      return agent.User[userTypeName + type]
    case 'individual':
      return agent.User[userTypeName + type]
    case 'foreignCompany':
      return agent.User[userTypeName + type]
  }
}

function getUserTypes(values) {
  const dispatchObj = {
    constants: userConstants.GetUserTypes,
    service: {
      func: agent.User.getUserTypes,
      params: values,
    },
  }
  return defAction(dispatchObj)
}

function changeProfileData(data) {
  const dispatchObj = {
    constants: userConstants.ChangeProfileData,
    service: {
      func: getAgentUserFunction(data.userType.name, 'Change'),
      params: data,
    },
  }
  return defAction(dispatchObj)
}

function getRoles(values) {
  const dispatchObj = {
    constants: userConstants.GetRoles,
    service: {
      func: agent.User.getRoles,
      params: values,
    },
  }
  return defAction(dispatchObj)
}
