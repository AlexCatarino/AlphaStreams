QuantConnect Alpha Stream Interaction via API (python edition)
==============================================================

What is it
----------

**quantconnect-alphastream** is a Python package providing interaction via API with `QuantConnect Alpha Streams <https://www.quantconnect.com/alpha>`_.

Installation Instructions
-------------------------
- Setup a `GitHub <https://github.com/>`_ account
- `Fork <https://help.github.com/articles/fork-a-repo/>`_ the `repository <https://github.com/QuantConnect/AlphaStream>`_ of the project
- Clone your fork locally
- `Installing from local src <https://packaging.python.org/tutorials/installing-packages/#installing-from-a-local-src-tree>`_ in Development Mode, i.e. in such a way that the project appears to be installed, but yet is still editable from the src tree.

 >>> git clone https://github.com/username/AlphaStream.git
 >>> python AlphaStream/setup.py install

Alternatively:

 >>> pip install git+https://github.com/username/AlphaStream.git


Enter Python's interpreter and type the following commands:

 >>> from AlphaStream import AlphaStreamRestClient
 >>> client = AlphaStreamRestClient(your-client-id, your-token)
 >>> insights = client.GetAlphaInsights(alpha-id)
 >>> for insight in insights:
 >>>     print(insight)

For your user id and token, please visit your `Organization Alpha Management Dashboard <https://www.quantconnect.com/terminal/#organization/alpha-management>`_.
