import {
	request, success, failure
}from './dispatchFunctions'

export const defAction = ({
	constants,
	service = null,
	reqParams = null,
	sucParams = null,
	sucFunction = null,
	sucFunctionDis = null,
	failParams = null
}) => {
	return (dispatch) => {
		dispatch(request(constants.REQUEST, reqParams))

		if (service){
			console.log('sertvice')
			service
				.func(service.params)
				.then((res) => {
					if (sucFunction)
						sucFunction()

					if (sucFunctionDis)
						dispatch(sucFunctionDis)

					dispatch(success(constants.SUCCESS, {
						sucParams,
						result: res
					}))
				})
				.catch((err) => {
					dispatch(failure(constants.FAILURE, {
						failParams,
						err
					}))
				})
		}
		else
			dispatch(success(constants.SUCCESS))
	}
}

export const defActionAlert = ({
	constant, reqParams = null
}) => {
	return (dispatch) => {
		dispatch(request(constant, reqParams))
	}
}
