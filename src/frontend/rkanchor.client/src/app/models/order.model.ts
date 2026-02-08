import { ClientOrder } from "./client.order.model";

export interface Order {
  id: number; 
  orderNo: string;
  orderDate: Date;
  producerType: string;
  additionalInfo: string;
  insertDate: Date;
  insertUser: string;
  updateDate: Date;
  updateUser: string;
  client: ClientOrder; 
}
