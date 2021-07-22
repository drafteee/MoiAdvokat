import {
  useEffect, useRef, useState 
} from 'react'

const useIO = (options) => {
  const [ element, setElement ] = useState(null)
  const [ entries, setEntries ] = useState([])
  const observer = useRef(null)

  const {
    root, rootMargin, threshold 
  } = options || {}

  useEffect(() => {
    if (element) {
      observer.current = new IntersectionObserver((ioEntries) => {
        setEntries(ioEntries)
      }, {
        threshold,
        root,
        rootMargin
      })

      observer.current.observe(element)
    }
    return () => {
      if (observer.current) {
        observer.current.disconnect()
      }
    }
  }, [ element, root, rootMargin, threshold ])

  return [ observer.current, setElement, entries ]
}

export default useIO
