import {
  globalConstants
} from './constants'

const language = window.localStorage.getItem('language')
let isRuInitial = 1
if (language) {
  isRuInitial = language === 'ru' ? 1 : 0
} else {
  window.localStorage.setItem('language', 'ru')
}

const initialState = {
  isRu: isRuInitial,
  isGlobalLoadingVisible: false,
}

export default function (state = initialState, action) {
  switch (action.type) {
    case globalConstants.CHANGE_LANGUAGE:
      window.localStorage.setItem('language', state.isRu === 1 ? 'be' : 'ru')
      return {
        ...state,
        isRu: state.isRu === 1 ? 0 : 1,
      }
    case globalConstants.SET_GLOBAL_LOADING_VISIBLE:
      return {
        ...state,
        isGlobalLoadingVisible: action.value,
      }
    default:
      return state
  }
}
