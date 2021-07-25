import {
	alertConstants
}from '../constants/'

const initialState = {
	error: null
}

export default function (state = initialState, action){
	//login;
	switch (action.type){
		case alertConstants.ERROR : {
			return {
				...state,
				error: action.payload.data ? action.payload : null
			}
		}
		default :
			return state
	}
}
