import { requests } from '../../../api/agent'

export default {
    getAdministrativeTerritories: params => requests.getWithParams(`/AdministrativeTerritory/GetAllCurrent`, params),
    getCountries: () => requests.get('/Country/GetAllCurrent')
}