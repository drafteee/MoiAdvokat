import {
  createStore, applyMiddleware, compose 
} from 'redux'
import thunk from 'redux-thunk'
import rootReducer from './reducers'
import {
  routerMiddleware 
} from 'react-router-redux'
import history from '../helpers/history'
import globalMiddleware from './middlewares/globalMiddleware'
import errorMiddleware from './middlewares/errorMiddleware'

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__
  ? window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__({})
  : compose

// const composeEnhancers = compose

const enhancer = composeEnhancers(
  applyMiddleware(thunk, routerMiddleware(history), globalMiddleware, errorMiddleware) //,signalRMiddleware),
  // other store enhancers if any
)

const inititalState = {}
const store = createStore(rootReducer, inititalState, enhancer)

export default store
