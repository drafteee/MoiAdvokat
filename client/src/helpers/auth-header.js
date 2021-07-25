export function authHeader(){
	// return authorization header with jwt token
	const user = JSON.parse(localStorage.getItem('user'))

	if (user){
		return {
			'Content-Type'  : 'application/json',
			'Authorization' : `Bearer ${ user.token }`
		}
	}
	return {
		'Content-Type': 'application/json'
	}
}

export function refreshToken(){
	const user = JSON.parse(localStorage.getItem('user'))

	return user.refreshToken
}