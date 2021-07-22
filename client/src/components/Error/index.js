import React, { 
  useState, useEffect 
} from 'react'
import { CloseOutlined } from '@ant-design/icons'
import {
  useSelector 
} from 'react-redux'
import './style.css'

const Error = () => {
  const error = useSelector((state)=> state.alertReducer.error)
  const [ isClose, setClose ] = useState(true)

  useEffect(()=>{
    //console.log(error)
    if(error)
      setClose(false)
  }, [ error ])
  return (
    <div className={ `error ${error && !isClose ? 'active' : ''}` }>
      {
        error?.data.code
      }
      {
        <div
          style={ { 
            width: '24px',
            float: 'right' 
          } }>
          
          {/* <ion-icon
            className='close'
            name='close-outline'
            onClick={ ()=> setClose(true) } /> */}
          <CloseOutlined
            className='close'
            onClick={ () => setClose(true)} />
              
          
        </div>
      }
      
    </div>
  )
}

export default Error