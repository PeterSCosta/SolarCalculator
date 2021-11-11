import styled from 'styled-components';
import { shade } from 'polished';

export const Header = styled.header`
  padding: 32px 0;
  background: #28262e;
`;

export const HeaderContent = styled.div`
  max-width: 1120px;
  margin: 0 auto;
  display: flex;
  align-items: center;
  justify-content: right;
`;

export const Profile = styled.div`
  display: flex;
  align-items: center;
  margin-left: 80px;

  div {
    display: flex;
    flex-direction: column;
    margin-left: 16px;
    line-height: 24px;

    span {
      color: #f4ede8;
    }

    a {
      text-decoration: none;
      color: #ff9000;
      &:hover {
        opacity: 0.8;
      }
    }
  }
`;

export const Container = styled.div``;

export const Content = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  form {
    display: flex;
    flex-direction: column;
    text-align: center;
    h1 {
      margin-bottom: 24px;
    }
    FormContent {
      display: flex;
      flex-direction: row;
    }
  }
    a {
      color: #f4ede8;
      display: block;
      margin-top: 24px;
      text-decoration: none;
      transition: color 0.2s;
      &:hover {
        color: ${shade(0.2, '#f4ede8')};
      }
    }
  }
`;

export const FormContent = styled.div``;

export const ContentColumn = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  max-width: 610px;
  div {
    max-width: 300px;
  }
`;
