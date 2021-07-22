export const request = (constant, payload) => { 
  return {
    type: constant,
    payload: payload 
  } 
}
export const success = (constant, payload) => { 
  return {
    type: constant,
    payload
  } 
}
export const failure = (constant, payload) => { 
  return {
    type: constant,
    payload
  } 
}