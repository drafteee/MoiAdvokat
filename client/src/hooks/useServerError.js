import React, {
  useEffect, useState 
} from 'react'
import {
  Alert 
} from 'antd'
import 'antd/lib/alert/style/index.css'

const objErrors = {
  403: 'Недостаточно прав - 403'
}

const useServerError = () => {
  const [ error, setError ] = useState(null)
  const [ errorView, setErrorView ] = useState(null)

  useEffect(() => {    
    if(error?.err){
      setErrorView( <Alert
        message={ `Произошла ошибка(${objErrors[error.err.status]})!` }
        type='warning' />)
    }else{
      setErrorView(null)
    }
  }, [ error ])

  return [ errorView, setError ]
}

export default useServerError
