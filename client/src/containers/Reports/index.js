
const serverUrl =
process.env.NODE_ENV === "development"
    ? process.env.SERVER_API_URL
    : `${ window.location.origin }/api`

const Reports = () => {
    return (
      <iframe height="1000" width="1000" src={`${ serverUrl }/Report/ReportAdmin?report=${ 1 }&year=${2021}`} />
    )
  }
  
  export default Reports