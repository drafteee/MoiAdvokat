// Warn if overriding existing method
if (Array.prototype.equals)
	console.warn('Overriding existing Array.prototype.equals. Possible causes: New API defines the method, there\'s a framework conflict or you\'ve got double inclusions in your code.')

// attach the .equals method to Array's prototype to call it on any array
Array.prototype.equals = function (array){
	// if the other array is a falsy value, return
	if (!array)
		return false

	// compare lengths - can save a lot of time 
	if (this.length != array.length)
		return false
	let flagI = false
	for (let i = 0, l=this.length; i < l; i++){
		// Check if we have nested arrays
		if (this[i] instanceof Array && array[i] instanceof Array){
			// recurse into the nested arrays
			if (!this[i].equals(array[i]))
				return false
		}
		else {
			let flagJ = false
			for (let j = 0, k=array.length; j < k; j++){
				if (this[i] == array[j]){
					// Warning - two different object instances will never be equal: {x:20} != {x:20}
					flagJ = true
					break
				}
			}
			flagI = flagJ
		}
	}
	return flagI
}

// Hide method from for-in loops
Object.defineProperty(Array.prototype, 'equals', {
	enumerable: false
})