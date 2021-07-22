function ParseAdminTerritory(adminTerr, isRu) {
  var fullAdminTerr = "";
  if (adminTerr.reLocalityType.position === 0) {
    fullAdminTerr +=
    (isRu || !adminTerr.nameBel) ? `${adminTerr.reLocalityType.nameShort} ${adminTerr.name}` : `${adminTerr.reLocalityType.nameShortBel} ${adminTerr.nameBel}`;
  } else {
    fullAdminTerr +=
    (isRu || !adminTerr.nameBel) ? `${adminTerr.name} ${adminTerr.reLocalityType.nameShort}` : `${adminTerr.nameBel} ${adminTerr.reLocalityType.nameShortBel}`;
  }
  if (adminTerr.parent) {
    let parent = ParseAdminTerritory(adminTerr.parent, isRu);
    fullAdminTerr += parent ? ` (${parent})` : ``;
  }
  return fullAdminTerr;
}
export default ParseAdminTerritory;
