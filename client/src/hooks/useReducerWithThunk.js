import {
  useReducer, useMemo, useEffect 
} from 'react'

let stores = {
}
let subscribers = {
}

const REDUX_DEVTOOL_SET_STATE = 'REDUX_DEVTOOL_SET_STATE'
const withDevTools = name => {
  return (
    name &&
    process.env.NODE_ENV === 'development' &&
    typeof window !== 'undefined' &&
    window.__REDUX_DEVTOOLS_EXTENSION__
  )
}

// Reducer to handle devtools actions.
const devToolReducer = reducer => (state, action) => {
  if (action.type === REDUX_DEVTOOL_SET_STATE) {
    return action.state
  } else {
    return reducer(state, action)
  }
}

function useReducerWithThunk(reducer, initialState, name) {
  let memoizedReducer = reducer
  let shouldConfigDevTools = withDevTools(name)

  // Memoizing to prevent recreation of devtoolReducer on each render.
  if (shouldConfigDevTools) {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    memoizedReducer = useMemo(() => devToolReducer(reducer), [ reducer ])
  }

  const [ state, dispatch ] = useReducer(memoizedReducer, initialState)

  useEffect(() => {
    if (shouldConfigDevTools) {
      if (stores[name]) {
        throw new Error('More than one useReducerWithThunk have same name')
      }

      // Initiale store from Redux devtools
      stores[name] = window.__REDUX_DEVTOOLS_EXTENSION__(
        reducer,
        initialState,
        {
          name: name
        }
      )

      subscribers[name] = stores[name].subscribe(() => {
        dispatch({
          type: REDUX_DEVTOOL_SET_STATE,
          state: stores[name].getState()
        })
      })
    }

    return () => {
      // Detach redux devtools
      if (shouldConfigDevTools) {
        subscribers[name]()
        subscribers[name] = undefined
        stores[name] = undefined
      }
    }
  }, [])

  const customDispatch = action => {
    if (typeof action === 'function') {
      return action(customDispatch)
    } else if (shouldConfigDevTools) {
      stores[name].dispatch(action)
    } else {
      dispatch(action)
    }
  }

  return [ state, customDispatch ]
}

export default useReducerWithThunk