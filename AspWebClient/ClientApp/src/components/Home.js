import React, { Component } from 'react';
import '../css/StyleSheet.css';

export class Home extends Component {
  static displayName = Home.name;
    constructor(props) {
        super(props);

        this.state = {
            orders: [],
            products: [],
            loading: true,
            orderResponse: "",
            productsReponse: ""
        };
    }
    componentDidMount() {
        this.fetchOrders();
    }
    async fetchOrders() {
        const response = await fetch('home/getorders');
        const data = await response.json();
        this.setState({ orders: data.orders, orderResponse: data.response, loading: false });
        this.fetchTop5Products();
    }
    async fetchTop5Products() {
        const response = await fetch('home/gettop5products');
        const data = await response.json();
        this.setState({ products: data.products, productsReponse: data.response, loading: false });
    }
  render () {
    return (
        <div>
            <br />
            <h1 id="tabelLabel" class="header-h1" >Orders with In Progress status</h1>
            <br />
            <div>
                <table style={{ borderBottom: "2px solid lightgray", flexGrow: 1 }} className="table" aria-labelledby="tabelLabel" >
                    <thead>
                        <tr>
                            <td><b>Order Id</b></td>
                            <td style={{ paddingRight: '4em' }}><b>Channel Order No</b></td>
                            <td><b>Merchant Order No</b></td>
                            <td><b>Email</b></td>
                            <td><b>OrderDate</b></td>
                            <td style={{ paddingRight: '10em' }}><b>Products</b></td>
                            <td style={{ paddingRight: '10em' }}><b>Status</b></td>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.orders.map(order => <><tr>
                            <td>{order.id}</td>
                            <td>{order.channelOrderNo}</td>
                            <td>{order.merchantOrderNo}</td>
                            <td>{order.email}</td>
                            <td>{order.orderDate}</td>
                            <td >{order.products.map(product => <><tr>
                                <td>{product.productName}</td>
                            </tr>
                            </>
                            )}
                            </td>
                            <td>{order.status}</td>
                        </tr>

                        </>
                        )}
                    </tbody>
                </table>
                <h1>{this.state.orderResponse}</h1>
            </div>
            <br />
            <h1 id="tabelLabel" class="header-h1">Top 5 Products</h1>
            <br />
            <div>
                <table style={{ borderBottom: "2px solid lightgray", flexGrow: 1 }} className="table" aria-labelledby="tabelLabel" >
                    <thead>
                        <tr>
                            <td><b>Product Name</b></td>
                            <td><b>GTIN</b></td>
                            <td><b>Quantity</b></td>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.products.map(product => <><tr>
                            <td>{product.productName}</td>
                            <td>{product.gtin}</td>
                            <td>{product.quantity}</td>
                        </tr>

                        </>
                        )}
                    </tbody>
                </table>
                <h1>{this.state.productsReponse}</h1>
            </div>
        </div>
    );
  }
}
