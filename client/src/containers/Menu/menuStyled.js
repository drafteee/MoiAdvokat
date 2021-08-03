import styled from 'styled-components'
import {
    Col, Layout
} from 'antd'

import {
    Link
} from 'react-router-dom'

const {
    Header
} = Layout

export const StyledUserNameSpan = styled.span`


  @media (max-width: 1080px) {
    font-size: medium;
  }
`


export const StyledHeaderDiv = styled.div`

${props => props.propp.isHome ? `@media (max-width: 1412px) {
.header{
height: 60vh;
     .header-img{
       height: 60vh;
     width: 100%;
     }
}
  }
  
  
  @media (max-width: 720px) {
  .header{
   height: 50vh;
     .header-img{
       height: 50vh;
     width: 100%;
     }
  }
   
   }` : ''};
   
   
        @media (max-width: 960px) {
        .sticky {
        .btn-language {
        margin-top: 0px;
        }
    }
    }
   
   
   @media (max-width: 836px) {
   .sticky.btn-language {
    height: 24px;
    padding: 0px 7px;
    font-size: 80%;
    border-radius: 2px;
    margin-top: 58px;
}
  .btn-language {
    height: 24px;
    padding: 0px 7px;
    font-size: 80%;
    border-radius: 2px;
    margin-top: 70px;
}

   @media (max-width: 520px) {
   .sticky.btn-language {

}
  .btn-language {
    font-size: 75%;
}


 @media (max-width: 755px) {
    .sticky .main-nav ul {
    margin-top: 24px;
    }
        }
    }
`;


export const StyledMobileMenuDiv = styled.div`
  @media (max-width: 960px) {

    ul{float: none};
  }
  @media (min-width: 961px) {
    .ant-menu-item-only-child{
    display: inline-block!important;
    } 
     #userSubmenu{
    display: inline-block!important;
    }
}
`


export const StyledLogoDiv = styled.div`
  
@media (max-width: 1412px) {
   width:85%;
 
  h1{font-size: 100%;}
   padding-left:0%;
  top: -3%;
 }
 
 @media (max-width: 1080px) {
  h1{font-size: 80%;}
 }
 
 
@media (max-width: 1285px) {
.hiddenLogoText{display: inline }
 }
  
@media (max-height: 440px) {
.hiddenLogoText{display: none }
 }
 
  @media (max-width: 275px) {
.hiddenMenuCenterText{display: none}
 }
 
   @media (max-width: 570px) {
.hiddenMenuCenterText{display: none}
 }
 
 
 @media (max-height: 580px) {
.hiddenMenuCenterText{display: none}
 }
 
 
  @media (max-width: 720px) {
  top: -9%;
  h1{font-size: 70%;}
 }
 
   @media (max-width: 627px) {
    h1{font-size: 70%;}
 }
`
export const StyledLogoCol = styled(Col)`
div {
height: 100px;
    width: auto;
    float: left;
    margin-top: 19px;
     font-size: 100%;
}

@media (max-width: 1285px) {
display: none;
}

 @media (max-width: 1340px) {
div{font-size: 80%}
 }

 
 @media (max-width: 1520px) {
img {
display: none
}
max-width: 21.1%;
flex: 0 0 21.1%
 }
`
export const StyledMenuCol = styled(Col)`

@media (max-width: 1520px) {
max-width: 70.5%;
flex: 0 0 70.5%
}

@media (max-width: 1285px) {
max-width: 91.6%;
flex: 0 0 91.6%
}

@media (max-width: 650px) {
max-width: 85.6%;
flex: 0 0 85.6%
}
`;


export const StyledMenuLink = styled(Link)`
@media (max-width: 1080px) {
    font-size: medium;
  }
`

export const StyledLanguageCol = styled(Col)`
 @media (max-width: 1520px) {
max-width: 8.4%;
flex: 0 0 8.4%
 }
 
 @media (max-width: 650px) {
max-width: 14.4%;
flex: 0 0 14.4%
}
`

