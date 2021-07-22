import {
  userConstants
} from '../constants/'
import {
  defAction
} from '../../helpers/defaultAction'
import agent from '../../api/agent'
import history from '../../helpers/history'

export const userActions = {
  login
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