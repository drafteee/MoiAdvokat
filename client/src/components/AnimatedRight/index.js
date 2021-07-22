import React, {
  useState, useEffect, memo, useCallback 
} from 'react'
import { 
  animated, useTransition 
} from 'react-spring/web.cjs'
const AnimatedRight = memo(({ 
  children, obj 
}) => {

  const [ index, setIndex ] = useState(obj)
  const [ isFirst, setIsFirst ] = useState(false)

  const setI = useCallback(() => setIndex(obj), [ obj ])
  const setIF = useCallback(() => setIsFirst(true), [ ])


  const transitions = useTransition(index, item=>item.key, {
    from: {
      opacity: isFirst ? 0 : 1,
    },
    enter: {
      opacity: 1,
    },
    onRest: ()=>{
      if(!isFirst && obj.key !== 0){
        setIF()
      }
    },
    leave: {
      opacity: isFirst ? 2 : 1,
    },
    config: {
      duration: 250
    }
  })
  
  useEffect(()=>{
    setI()
  }, [ obj, setI ])

  return (
    <>
      { transitions.map(({
        item, props, key 
      }) => {
        return (
          <animated.div
            style={ {
              opacity: props.opacity.interpolate([ 0, 0.5, 1, 1.5, 2 ], [ 0, 0, 1, 0, 0 ]),
              // minHeight: '300px' 
            } }
            key={ key }>
            {
              React.Children.map(children, child => {
                return React.cloneElement(child, { 
                  item: item ? item : null 
                } )
              })
            }
          </animated.div>
        )
      }) }
    </>
  )
})

export default AnimatedRight