import Cidade from './Cidade';

export default class Estado {
    public id?:Number;
    public nome?:String;
    public UF?:String;
    public cidades?: Array<Cidade>;
}