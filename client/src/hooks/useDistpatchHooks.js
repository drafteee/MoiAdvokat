import {
  useDispatch 
} from 'react-redux'

export default function useDispatchToStore(func) {
  const dispatch = useDispatch()
  return (payload) => {
    dispatch(func(payload))
  }
}