import { requests } from "../../../api/agent";

export default {
    getAll: () => requests.get('/Lawyer/GetAll'),
    getAllCurrent: () => requests.get('/Lawyer/GetAllCurrent'),
    uploadCertificate: params => requests.post('/Lawyer/UploadCertificate', params),
    checkIfCertificateExists: params => requests.getWithParams('/Lawyer/CheckIfCertificateExists', params)
};
