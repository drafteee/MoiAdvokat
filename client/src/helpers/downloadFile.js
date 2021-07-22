export default function downloadFile(response, file){
	if (window.navigator && window.navigator.msSaveOrOpenBlob){
		// IE variant
		window.navigator.msSaveOrOpenBlob(new Blob([response]), file.fileName + file.fileExtension)
	}
	else {
		const url = window.URL.createObjectURL(new Blob([response]))
		const link = document.createElement("a")
		link.href = url
		link.setAttribute("download", file.fileName + file.fileExtension)
		document.body.appendChild(link)
		link.click()
		link.remove()
	}
	return true
}
