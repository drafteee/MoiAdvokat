import { homeConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const homeActions = {
  sendFeedback,
  getStatistics
};

function sendFeedback(feedback) {
  return (dispatch) => {
    dispatch(request(feedback));
    // userService.sendFeedback(feedback)
    //   .then(res => {
    //     return res.json()
    //   }).then(res => {
    //     dispatch(success(res.data))
    //   })
    //   .catch(err => {
    //     dispatch(failure(err.response.data))
    //   })

    function request(user) {
      return {
        type: homeConstants.SEND_FEEDBACK_REQUEST,
        payload: user,
      };
    }
    function success(user) {
      return {
        type: homeConstants.SEND_FEEDBACK_SUCCESS,
        payload: user,
      };
    }
    function failure(error) {
      return {
        type: homeConstants.SEND_FEEDBACK_FAILURE,
        payload: error,
      };
    }
  };
}

function getStatistics() {
  const dispatchObj = {
    constants: homeConstants.GetStatistics,
    service: {
      func: api.getStatistics
    }
  };

  return defAction(dispatchObj);
}
