import { globalConstants } from "./constants";

export const globalActions = {
  changeLanguage,
  setGlobalLoadingVisible,
};

function changeLanguage() {
  return (dispatch) => {
    dispatch(change());

    function change() {
      return {
        type: globalConstants.CHANGE_LANGUAGE,
      };
    }
  };
}

function setGlobalLoadingVisible(value) {
  return (dispatch) => {
    dispatch(change(value));

    function change(value) {
      return {
        type: globalConstants.SET_GLOBAL_LOADING_VISIBLE,
        value: value,
      };
    }
  };
}
