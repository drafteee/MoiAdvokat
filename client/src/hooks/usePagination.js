import {
  useEffect, useState, useCallback 
} from 'react'

import {
  useHistory
} from 'react-router-dom'
import * as QueryString from 'query-string'


const usePagination = (options) => {
  let history = useHistory()

  const getPaginationProps = useCallback(() =>{
    let routeParams = QueryString.parse(history.location.search,{
      arrayFormat: 'comma',
      parseBooleans: true
    })

    routeParams = Object.keys(routeParams).reduce((accum, curr)=>{
      if( ![ 'current', 'pageSize' ].includes(curr)){
        return {
          ...accum,
          [curr]: routeParams[curr] ? Array.isArray(routeParams[curr]) ? [ ...routeParams[curr] ] : [ routeParams[curr] ] : []
        }
      }else{
        return {
          ...accum,
          [curr]: routeParams[curr]
        }
      }
    }, {
      descend: [],
      ascend: []
    })

    return routeParams
  }, [ history.location.search ])

  const [ routeObject, setRouteObject ] = useState({
    isNeedSetURL: false
  })

  const reset = () =>{    
    if(routeObject.pagination === undefined){
      const routeParams = getPaginationProps()
      let newRouteObject = {
        pagination: {
          current: routeParams.current,
          pageSize: routeParams.pageSize
        },
        sorter: [],
        filters: {}
      }
      const obj = Object.keys(routeParams).reduce((accum, next)=>{
        if(![ 'current', 'pageSize' ].includes(next))
        {
          if(next === 'ascend' || next === 'descend'){
            let sorterObj = null
            if(Array.isArray(routeParams[next])){
              sorterObj = routeParams[next].reduce((acc, val)=>{
                return[
                  ...acc,
                  {
                    order: next,
                    field: val
                  }
                ]
              },[])
            }else{
              sorterObj = {
                order: next,
                field: routeParams[next]
              }
            }
            return{
              ...accum,
              sorter: Array.isArray(routeParams[next]) ? 
                [
                  ...accum.sorter,
                  ...sorterObj
                ] : [
                  ...accum.sorter,
                  sorterObj
                ]
            }
          }
    
          return{
            ...accum,
            filters: {
              ...accum.filters,
              [next]: routeParams[next]
            }
          }
    
        }
        return{
          ...accum
        }
      }, { 
        ...newRouteObject 
      })
      setRouteObject({ 
        ...obj,
        isNeedSetURL: false 
      })  
    }
    
  }

  const updateField = (parent, key, value) =>{
    setRouteObject({
      ...routeObject,
      isNeedSetURL: true,
      [parent ? parent : key]: parent ? {
        ...routeObject[parent],
        [key]: value
      } : 
        value
    })
  }
  const {
    pagination, filters, sorter, extra, isNeedSetURL 
  } = options || {}

  useEffect(() => {
    setRouteObject({
      pagination, 
      filters, 
      sorter, 
      extra,
      isNeedSetURL 
    })
  }, [ pagination, filters, sorter, extra, isNeedSetURL ])

  useEffect(() => {

    if(routeObject.isNeedSetURL)
    {
      const sortArray = Array.isArray(routeObject.sorter) ? [ ...routeObject.sorter ] : [ routeObject.sorter ]
      const sortObj = sortArray.reduce((accumulator, currentValue)=>{
        if(currentValue.order === 'descend'){
          return {
            ...accumulator,
            descend: [ ...accumulator.descend ?? [], currentValue.field ]
          }
        }else if(currentValue.order === 'ascend'){
          return {
            ...accumulator,
            ascend: [ ...accumulator.ascend ?? [], currentValue.field ]
          }
        }
        return accumulator
      }, {})

      let routeString = `?${QueryString.stringify({
        current: routeObject.pagination.current,
        pageSize: routeObject.pagination.pageSize,
        ...sortObj,
        ...routeObject.filters
      }, { 
        arrayFormat: 'comma',
        skipNull: true,
        skipEmptyString: true  
      })}`

      history.push({
        search: routeString
      })
    }
      
  }, [ history, routeObject, getPaginationProps ])

  return [ setRouteObject, getPaginationProps, updateField, reset ]
}

export default usePagination
