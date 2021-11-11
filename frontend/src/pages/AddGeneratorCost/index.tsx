import React, { useCallback, useRef } from 'react';
import { FormHandles } from '@unform/core';
import { Form } from '@unform/web';
import * as Yup from 'yup';

import { FiPower, FiEdit } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';
import getValidationError from '../../utils/getValidationErrors';

import { useToast } from '../../hooks/toast';
import api from '../../services/api';
import { useAuth } from '../../hooks/auth';

import {
  Container,
  Header,
  HeaderContent,
  Profile,
  Content,
  FormContent,
  ContentColumn,
} from './styles';

import Button from '../../components/Button';
import Input from '../../components/Input';

interface AddGeneratorCostFormData {
  cost: number;
}

const AddGeneratorCost: React.FC = () => {
  const { user, signOut } = useAuth();
  const formRef = useRef<FormHandles>(null);
  const history = useHistory();
  const { addToast } = useToast();

  const handleSubmit = useCallback(
    async (data: AddGeneratorCostFormData) => {
      try {
        formRef.current?.setErrors({});
        const schema = Yup.object().shape({
          cost: Yup.number()
            .required('Custo obrigatória')
            .moreThan(0.1, 'Custo deve ser maior que 0.1'),
        });

        await schema.validate(data, {
          abortEarly: false,
        });

        await api.post('/v1/generatorcosts', data);

        history.push('/dashboard');
      } catch (err) {
        if (err instanceof Yup.ValidationError) {
          const errors = getValidationError(err);

          formRef.current?.setErrors(errors);

          return;
        }

        addToast({
          type: 'error',
          title: 'Erro no cadastro',
          description: 'Ocorreu um erro ao cadastrar custo',
        });
      }
    },
    [addToast, history],
  );

  return (
    <Container>
      <Header>
        <HeaderContent>
          <Profile>
            <div>
              <span>
                Bem-vindo,
                <strong>{user.userName}</strong>
              </span>
            </div>
          </Profile>
          <button type="button" onClick={signOut}>
            <FiPower />
          </button>
        </HeaderContent>
      </Header>
      <Content>
        <h1>Adicionar novo custo</h1>
        <Form ref={formRef} onSubmit={handleSubmit}>
          <FormContent>
            <ContentColumn>
              <Input name="cost" icon={FiEdit} placeholder="Custo KW/h" />
            </ContentColumn>
          </FormContent>
          <Button type="submit">Adicionar</Button>
        </Form>
      </Content>
    </Container>
  );
};

export default AddGeneratorCost;
