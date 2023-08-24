import { PropertyValueGetterPipe } from './property-value-getter.pipe';

describe('PropertyValueGetterPipe', () => {
  it('create an instance', () => {
    const pipe = new PropertyValueGetterPipe();
    expect(pipe).toBeTruthy();
  });

  it('should return property value from onInject', () => {
    const pipe = new PropertyValueGetterPipe();
    const input = { id: 1, name: 'Hello world' };

    const nameOutput = pipe.transform(input, 'name');
    const idOutput = pipe.transform(input, 'id');

    expect(nameOutput).toEqual(input.name);
    expect(idOutput).toEqual(input.id);
  });
});
