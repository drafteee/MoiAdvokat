/*
 * проверяет нужно ли загружать файлы, так как в choseFiles храняться как уже загруженные, 
 * так и загружаемые на сервер (актуально в случаи с резолюцией)
 */
const CheckFileForUpload = (needToUpload, fileList) => {
	fileList.forEach(item => {
		//если есть файл без ссылки, прокидываем его на сервер
		if (!item.url)
			needToUpload = true
	})
	return needToUpload
}
export { CheckFileForUpload }